using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public float maxLaughAmount = 100f;

    public float currentLaughAmount = 0;

    public float increment = 0.1f;

    public float decrement = 0.01f;

    public bool gameWon = false;

    public Action OnGameWin;

    public Image fillImage;

    [SerializeField] private GameObject image;
    [SerializeField] private GameObject gameWin;

    private void Awake()
    {
        instance = this;
    }

    private void Start() {
        StartCoroutine(ReduceProgress(0.5f));
    }

    public void Update() {
        //AddProgress();
    }

    public IEnumerator ReduceProgress(float time) {
        yield return new WaitForSeconds(time);
        if (currentLaughAmount > 0f) { 
            currentLaughAmount -= decrement;
            fillImage.fillAmount = Map(currentLaughAmount, 0, maxLaughAmount, 0, 1);
            StartCoroutine(ReduceProgress(0.5f));
        }
    }

    public void AddProgress() {
        currentLaughAmount += increment;
        fillImage.fillAmount = Map(currentLaughAmount, 0, maxLaughAmount, 0, 1);
        if (currentLaughAmount >= maxLaughAmount && !gameWon) {
            gameWon = true;
            OnGameWin?.Invoke();
            gameWin.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax) {
        // Ensure the input value is within the original range
        float clampedValue = Mathf.Clamp(value, fromMin, fromMax);

        // Calculate the remapped value
        float mappedValue = toMin + (toMax - toMin) * ((clampedValue - fromMin) / (fromMax - fromMin));

        return mappedValue;
    }

    public void GameFailed()
    {
        image.SetActive(true);
        Time.timeScale = 0;
    }
}
