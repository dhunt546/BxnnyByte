using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class debris : MonoBehaviour
{
    ScoreManager score;

    private void Start()
    {
        score = FindObjectOfType<ScoreManager>();
    }
    public void CleanDebris()
    {       
        score.AddToScore(1,0.1f);
        Destroy(gameObject);
    }
}
