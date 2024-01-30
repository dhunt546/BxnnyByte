using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private bool isGamePaused = false;
    public GameObject optionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        // Pause the game
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        // Unpause the game
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void OpenOptionsMenu()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
            PauseGame();
        }
    }

    public void CloseOptionsMenu()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
            UnpauseGame();
        }
    }


}
