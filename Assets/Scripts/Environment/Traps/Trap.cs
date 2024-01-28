using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour
{
    [SerializeField] private TrapTypeSO trapType;
    [SerializeField] private UnityEvent response;
    [SerializeField] private Rigidbody trapRB;
    [SerializeField] private int trapResetTime = 10;

    private Vector3 defaultPosition;

    private void Awake()
    {
        if(!trapRB)
        {
            print("Rigidbody wasn't assigned");
        }
        if(TryGetComponent(out trapRB))
        {
        }
        defaultPosition = transform.position;
    }

    private void OnEnable()
    {
        TrapActivationEvent.Instance.AddListener(OnTrapActivatedEvent);
    }

    private void OnTrapActivatedEvent(TrapTypeSO trapType)
    {
        if(this.trapType == trapType)
        {
            ActivateTrap();
        }
    }

    private void ActivateTrap()
    {
        response.Invoke();
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("something was triggered");
        if(other.TryGetComponent(out Istunable stunable))
        {
            stunable.OnStunned();
            print($"{stunable} was stunned");
        }
        gameObject.SetActive(false);
        Invoke(nameof(ResetTrap),trapResetTime);

    }

    private void ResetTrap()
    {
        transform.position = defaultPosition;
        trapRB.useGravity = false;
        trapRB.velocity = Vector3.zero;
        gameObject.SetActive(true);
        GetComponent<Collider>().enabled = false;
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }
}
