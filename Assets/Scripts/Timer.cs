using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float countdownTime = 300f;  // Set the initial countdown time in seconds (5 minutes in this example)
    public TMP_Text countdownText;  // Assign the UI Text component in the Inspector

    private float currentTime;

    void Start()
    {
        currentTime = countdownTime;
        UpdateUI();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            // Countdown reached zero, you can add additional logic here
            UIManager.instance.GameFailed();
            Debug.Log("Countdown reached zero!");
        }
    }

    void UpdateUI()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Display the current time in the UI Text component
        if (countdownText != null)
        {
            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            Debug.LogError("UI Text component not assigned!");
        }
    }
}
