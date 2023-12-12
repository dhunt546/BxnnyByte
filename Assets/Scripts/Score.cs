using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

public class Score : MonoBehaviour
{
    public static Score Instance;
    public TextMeshProUGUI scoreText;
    private int score;
    public int scoreAdded;

    void Start()
    {
        UpdateScoreDisplay();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void IncrementScore()
    {
       scoreAdded++;
        SetScore(scoreAdded);
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public int GetScore()
    {
        return scoreAdded;
    }
}
