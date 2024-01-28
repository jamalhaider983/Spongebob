using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public LayerMask groundLayer;
    public bool isGrounded = false;


    public void OnTriggerEnter(Collider other) {
        if (IsOnLayer(other.gameObject, groundLayer)) {
            isGrounded = true;
            return;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (IsOnLayer(other.gameObject, groundLayer)) {
            isGrounded = false;
            return;
        }
    }

    //public void OnCollisionStay(Collision collision) {
    //    foreach (ContactPoint contact in collision.contacts) {
    //        if (IsOnLayer(collision.gameObject, groundLayer)) {
    //            isGrounded = true;
    //            return; // Exit the loop as soon as one ground contact is found
    //        }
    //    }

    //    isGrounded = false;
    //}

    bool IsOnLayer(GameObject obj, LayerMask layer) {
        // Get the layer of the GameObject
        int objLayer = obj.layer;

        // Use bitwise AND to check if the GameObject's layer is in the specified layer mask
        return (layer.value & (1 << objLayer)) != 0;
    }
}
