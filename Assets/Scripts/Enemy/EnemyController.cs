using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, Istunable
{
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
    private bool isPlayerIntimidating;
    private bool isStunned;

    private void Awake()
    {
        //if(!playerController) 
        //playerController =
        playerTransform = playerController.transform;
    }

    private void Start()
    {
       
    }

    private void FixedUpdate()
    {
        IsPlayerChaseInRange();
        IsPlayerInAttackRange();
        IsPlayerIntimidating();
        UpdateState();
        
    }

    private void LateUpdate()
    {
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
        if(isChasing || isPlayerIntimidating)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }
        else if(!isAttacking)
        {
            navMeshAgent.SetDestination(defaultStayTransform.position);
        }
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
        }
        else
        {
            isAttacking = false;
        }
    }

    public void IsPlayerIntimidating()
    {
        isPlayerIntimidating = playerController.IsIntimidating;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
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
    }

    private IEnumerator ResetStun()
    {
        yield return new WaitForSeconds(stuntDuration);
        isStunned = false;
        navMeshAgent.speed = 3.5f;
    }
}
