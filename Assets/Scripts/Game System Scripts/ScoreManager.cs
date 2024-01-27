using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

 public static class ScoreManager
{
    private static Score scoreInstance;
    public static void Initialize(Score score)
    {
        scoreInstance = score;
    }
    public static void AddToScore(int pointsAdded, float playerExperience)
    {
        PlayerStats.PlayerLevel += playerExperience;
        PlayerStats.Score += pointsAdded;
       

        if (scoreInstance != null)
        {
            
            scoreInstance.UpdateScoreDisplay();
        }
        
    }
}
