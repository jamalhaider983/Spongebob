using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour {

    public void Collect() {
        gameObject.SetActive(false);
    }
}
