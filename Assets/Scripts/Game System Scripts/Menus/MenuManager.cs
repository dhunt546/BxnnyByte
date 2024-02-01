
using UnityEngine;
using UnityEngine.SceneManagement;
public static class MenuManager
{
    public static bool isGamePaused = false;
    
    public static  void PauseGame()
    {
        // Pause the game
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public static void UnpauseGame()
    {
        // Unpause the game
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public static void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public static void OpenOptionsMenu(GameObject optionsPanel)
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
            PauseGame();
        }
    }

    public static void CloseOptionsMenu(GameObject optionsPanel)
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
            UnpauseGame();
        }
    }
    public static void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
