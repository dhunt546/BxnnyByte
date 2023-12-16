using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class debris : MonoBehaviour
{
    public ScoreManager score;

    private void Start()
    {
        score = GetComponent<ScoreManager>();
    }
    public void CleanDebris()
    {       
        score.IncrementScore();
        Destroy(gameObject);
    }
}
