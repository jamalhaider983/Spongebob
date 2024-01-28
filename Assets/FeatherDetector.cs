using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherDetector : MonoBehaviour
{
    public LayerMask featherLayer;
    public Action OnFeatherDetected;
    public bool hasFeather;
    public Action<bool> OnFeatherUpdated;

    //public void OnCollisionEnter(Collision collision) {

    //    Debug.Log("Colliding");
    //    if (IsOnLayer(collision.gameObject, featherLayer)) {
    //        Debug.Log("Hitting Layer");
    //    }
    //}

    public void Start() {
        FeatherManager.OnPlaceFeather += LooseFeather;
    }

    public void LooseFeather() {
        hasFeather = false;
        OnFeatherUpdated?.Invoke(hasFeather);
    }

    public void OnTriggerEnter(Collider other) {
        if(IsOnLayer(other.gameObject, featherLayer)) {
            Feather feather = other.gameObject.GetComponent<Feather>();
            feather.Collect();
            hasFeather = true;
            OnFeatherUpdated(hasFeather);
        } 
    }

    bool IsOnLayer(GameObject obj, LayerMask layer) {
        // Get the layer of the GameObject
        int objLayer = obj.layer;

        // Use bitwise AND to check if the GameObject's layer is in the specified layer mask
        return (layer.value & (1 << objLayer)) != 0;
    }

}
