using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadStartScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }
}
