using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class debris : MonoBehaviour
{
    public ScoreManager score;

    public void CleanDebris()
    {       
        score.IncrementScore();
        Destroy(gameObject);
    }
}
