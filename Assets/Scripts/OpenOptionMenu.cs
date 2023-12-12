using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOptionMenu : MonoBehaviour
{
    // Reference to the options menu panel
    public GameObject optionsPanel;

    // Called when the options button is pressed
    public void ShowOptionsMenu()
    {
        // Set the options menu panel to active
        optionsPanel.SetActive(true);
    }

    // Called when the close button in the options menu is pressed
    public void CloseOptionsMenu()
    {
        // Set the options menu panel to inactive
        optionsPanel.SetActive(false);
    }
}
