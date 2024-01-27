using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        ScoreManager.Initialize(this);
        }
    public void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = PlayerStats.Score.ToString();
        }
    }
}
