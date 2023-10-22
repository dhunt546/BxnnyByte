using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    public int scenceBuilderIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {

            SceneManager.LoadScene(scenceBuilderIndex, LoadSceneMode.Single);
        
        }


    }



}
