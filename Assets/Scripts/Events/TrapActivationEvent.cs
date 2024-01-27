using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapActivationEvent : UnityEvent<TrapTypeSO>
{
    public static TrapActivationEvent Instance = new TrapActivationEvent();
}
