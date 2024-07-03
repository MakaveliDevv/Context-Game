using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    public float elapsedGameplayTime = 0f;
    public TextMeshProUGUI timerText; 

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Ensure that this object persists between scenes
    }

    void Update()
    {
        elapsedGameplayTime += Time.deltaTime;
        UpdateTimerText(elapsedGameplayTime);
    }

    void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
