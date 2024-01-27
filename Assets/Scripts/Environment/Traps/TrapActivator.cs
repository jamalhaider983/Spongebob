using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    [SerializeField] private TrapTypeSO trapToActivate;

    public void ActivateTrap()
    {
        TrapActivationEvent.Instance.Invoke(trapToActivate);
        print("Event Invoked");
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Collision Enter");
        if(other.TryGetComponent(out PlayerController component))
        {
            ActivateTrap();
        }
    }
}
