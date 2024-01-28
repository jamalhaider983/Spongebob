using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, Istunable
{
    [SerializeField] private PlayerController[] players;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform defaultStayTransform;
    [SerializeField] private Animator anim;
    private Transform playerTransform;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int chaseStopDuration = 3;
    [SerializeField] private float stuntDuration = 5f;

    private bool isChasing;
    private bool isAttacking;
    [SerializeField] private bool isPlayerIntimidating;
    private bool isStunned;

    [SerializeField] private AudioClip stunnedSFX;

    private void Awake()
    {
        //if(!playerController) 
        //playerController =
        playerTransform = playerController.transform;
    }

    private void Start()
    {
       IntimidateEvent.Instance.AddListener(OnIntimidate);
    }

    private void OnIntimidate(PlayerTypeSO playerType)
    {
        print("Player Intimidate event recieved :" + playerType);
        foreach(PlayerController player in players)
        {
            if(player.playerType == playerType)
            {
                navMeshAgent.SetDestination(player.transform.position);
                print("Destination Set");
                return;
            }
            else
            {
                print("Type Not matched");
            }
        }
    }

    private void FixedUpdate()
    {
       // IsPlayerChaseInRange();
        IsPlayerInAttackRange();
        //IsPlayerIntimidating();
       // UpdateState();
        
    }

    private void LateUpdate()
    {
        if(isStunned)
        {
            anim.SetBool("Stunned",true);
        }
        else
        {

            anim.SetBool("Stunned",false);
        }
        if(Vector3.Distance(transform.position,navMeshAgent.destination)>0.5f)
        {
            anim.SetFloat("Speed",1);
        }
        else
        {
            anim.SetFloat("Speed",0);
        }
        
    }

    private void UpdateState()
    {
        // if(isChasing || isPlayerIntimidating)
        // {
        //     print("Chasing");
        //     navMeshAgent.SetDestination(playerTransform.position);
        // }
        // else if(!isAttacking)
        // {
        //     navMeshAgent.SetDestination(defaultStayTransform.position);
        // }
    }

    public bool IsPlayerChaseInRange()
    {
        if(Vector3.Distance(transform.position,playerTransform.position)<=chaseRange && !isAttacking)
        {
            isChasing = true;
            return true;
        }
        else
        {
            StartCoroutine(StopChasingPlayer());
            return false; 
        }
    }

    private IEnumerator StopChasingPlayer()
    {
        yield return new WaitForSeconds(chaseStopDuration);
         if(!IsPlayerChaseInRange() && !isAttacking && !isPlayerIntimidating)
             isChasing = false;
    }

    public void IsPlayerInAttackRange()
    {
        if(Vector3.Distance(transform.position,playerTransform.position)<=attackRange)
        {
            isChasing = false;
            isAttacking = true;
            anim.SetBool("Attack",true);
        }
        else
        {
            isAttacking = false;
            anim.SetBool("Attack",false);
        }
    }

    public void IsPlayerIntimidating()
    {
        isPlayerIntimidating = playerController.IsIntimidating;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        IntimidateEvent.Instance.RemoveListener(OnIntimidate);
    }

    public void OnStunned()
    {
        print("Boss is stunned");
        isStunned = true;
        OnGetStunned();
        StartCoroutine(ResetStun());
    }

    private void OnGetStunned()
    {
        //navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
        AudioSource.PlayClipAtPoint(stunnedSFX,Camera.main.transform.position);
    }

    private IEnumerator ResetStun()
    {
        yield return new WaitForSeconds(stuntDuration);
        isStunned = false;
        navMeshAgent.speed = 3.5f;
    }

    private void OnTriggerStay(Collider other)
    {
        print(other.name);
        if(other.CompareTag("Feather"))
        {
            if(isStunned)
            UIManager.instance.AddProgress();
        }
    }


}
