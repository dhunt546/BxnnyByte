using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreDisplay : MonoBehaviour
{
    public static int score;
    public TextMeshProUGUI scoreText;

    private void Update()
    {
        UpdateScoreDisplay();
    }
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
