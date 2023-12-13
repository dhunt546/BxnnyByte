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
  
    public int scoreAdded;

 //   public GameObject debris;
 //   public GameObject Swarmer;
 //   public GameObject Brute;
 //   public GameObject PossessedHost;
 //   public GameObject HostMother;

    void Start()
    {
        UpdateScoreDisplay();
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
