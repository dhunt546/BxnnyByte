using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour
{
    public GameObject optionsPanel;
    public void OpenSettingsInGame()
    {
        MenuManager.OpenOptionsMenu(optionsPanel);
    }

    public void LoadNewScene(string sceneName)
    {
        MenuManager.LoadScene(sceneName);
    }
    public void ExitGame()
    {
        MenuManager.ExitGame();
    }
    public void CloseOptionsInstance()
    {
        MenuManager.CloseOptionsMenu(optionsPanel);
    }
    public void FullScreenInstance()
    {
        MenuManager.ToggleFullscreen();
    }

}
