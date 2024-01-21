using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    
    public TextMeshProUGUI scoreText;
    public PlayerStats playerStats;
    void Start()
    {
        UpdateScoreDisplay();
    }

    public void AddToScore(int newScore, float playerExperience)
    {
        playerStats.PlayerLevel += playerExperience;
        score += newScore;
        UpdateScoreDisplay();
    }

    public void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
