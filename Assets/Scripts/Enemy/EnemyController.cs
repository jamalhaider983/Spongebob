using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform defaultStayTransform;
    private Transform playerTransform;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int chaseStopDuration = 3;

    private bool isChasing;
    private bool isAttacking;
    private bool isPlayerIntimidating;

    private void Awake()
    {
        playerTransform = playerController.transform;
    }

    private void Start()
    {
        InvokeRepeating(nameof(IsPlayerChaseInRange),0,0.5f);
        InvokeRepeating(nameof(IsPlayerInAttackRange),0,0.5f);
        InvokeRepeating(nameof(IsPlayerIntimidating),0,0.5f);
        InvokeRepeating(nameof(UpdateState),0,0.5f);
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
            StopChasingPlayer();
            return false; 
        }
    }

    private async void StopChasingPlayer()
    {
        await Task.Delay(chaseStopDuration * 1000);
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
        CancelInvoke();
    }
}
