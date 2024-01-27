using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour
{
    [SerializeField] private TrapTypeSO trapType;
    [SerializeField] private UnityEvent response;
    [SerializeField] private float trapResetTime = 10f;

    private Vector3 defaultPosition;

    private void Awake()
    {
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
    }

    private void OnTriggerEnter(Collider other)
    {
        print("something was triggered");
        if(other.TryGetComponent(out Istunable stunable))
        {
            stunable.OnStunned();
            print($"{stunable} was stunned");
        }
        StartCoroutine(ResetTrap());
        gameObject.SetActive(false);

    }

    private IEnumerator ResetTrap()
    {
        yield return new WaitForSeconds(trapResetTime);
        transform.position = defaultPosition;
        gameObject.SetActive(true);
    }
}
