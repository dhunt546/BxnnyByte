using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

public static class ScoreManager
{

    public static  TextMeshProUGUI scoreText;
    public static void AddToScore(int pointsAdded, float playerExperience)
    {
        PlayerStats.PlayerLevel += playerExperience;
        PlayerStats.Score += pointsAdded;
        UpdateScoreDisplay();
    }

    public static void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = PlayerStats.Score.ToString();
        }
    }
}
