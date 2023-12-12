using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour      //Created by Regan Ly.
                                        //Tinkered around with, by Devin Hunt.
{
    public float timeRemaining = 0;
    public static bool timeIsRunning = true;
    public TMP_Text timeText;
    public GameObject CountdownTimer;

    void Awake()
    {
        timeIsRunning = true; 
    }

    void Update()
    {
        DisplayTime(timeRemaining);

        if (timeIsRunning == true)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                //DisplayTime(timeRemaining);
            }
            if (timeRemaining <= 0)
            {
                CountdownTimer.SetActive(false);
                timeRemaining = 0;
                timeIsRunning = false;
                SceneManager.LoadScene("Game Over");               
            }
        }     
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay -= 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //float milliseconds = Mathf.FloorToInt((timeToDisplay * 1000) % 100); // Calculate milliseconds
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //, milliseconds);
    }
}
