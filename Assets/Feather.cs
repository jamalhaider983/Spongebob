using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour {

    public Action OnCollected;

    public void Collect() {
        OnCollected?.Invoke();
    }

    public void SetNewPosition(Vector3 position) { 
        transform.position = position;
    }
}
