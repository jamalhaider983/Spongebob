using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherManager : MonoBehaviour {

    public bool featherTaken;
    public Feather feather;
    public Transform[] spawnPoints;
    public static Action OnPlaceFeather;

    private Feather featherInstance;


    private void Start() {
        featherInstance = Instantiate(feather);
        StartCoroutine(PlaceFeather(0f));
    }

    public IEnumerator PlaceFeather(float time) {
        yield return new WaitForSeconds(time);
        featherTaken = false;
        OnPlaceFeather?.Invoke();
        featherInstance.gameObject.SetActive(true);
        Vector3 newPosition = GetRandomSpawnPoint();
        featherInstance.SetNewPosition(newPosition);
        featherInstance.OnCollected += RemoveFeather;
    }

    public void RemoveFeather() {
        featherTaken = true;
        featherInstance.gameObject.SetActive(false);
        StartCoroutine(PlaceFeather(10f));
    }

    public Vector3 GetRandomSpawnPoint() {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position;
    }
}
