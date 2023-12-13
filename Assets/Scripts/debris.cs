using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class debris : MonoBehaviour
{
    public ScoreManager score;
    //add to score
    //remove debris
    // Start is called before the first frame update


    public void CleanDebris()
    {
        //Debug.Log("destroy object");
        score.IncrementScore();
        Destroy(gameObject);
    }
}
