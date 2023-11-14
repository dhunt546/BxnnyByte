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
    private int score = 0000;
    public int SpiderHP = 1;
    void Start()
    {
        scoreText.text = score.ToString();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    void Update()
    {
        if (SpiderHP == 0) ;
        {
            score += 100;
        }
    }
}
