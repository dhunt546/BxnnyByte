using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour        //Edited by Devin Hunt.
{
    int currentSceneIndex;
    int previousSceneIndex;

    private void Start()
    {

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (currentSceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            previousSceneIndex = currentSceneIndex;
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
    }

    public void RestartLevel()
    {
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

    public void LoadGameOver()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousSceneIndex);
    }
}
