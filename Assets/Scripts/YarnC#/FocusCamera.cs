using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;
using Yarn.Unity;

public class FocusCamera : MonoBehaviour 
{
    //Created by Devin Hunt.
    //Source: https://docs.yarnspinner.dev/using-yarnspinner-with-unity/creating-commands-functions
    //Source: https://docs.yarnspinner.dev/unity-tutorial-projects/example-project-3

    // Drag and drop Dialogue Runner into this variable.
    DialogueRunner dialogueRunner;

    // Drag and drop desired Camera into this varible.
    public GameObject newCamera;

    void Awake()
    {   // Initalize DialogueRunner. 
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
       
        // Adds selected Method to end of all Conversations. 
        dialogueRunner.onDialogueComplete.AddListener(CameraDoneLoooking);
    }

    // Yarnspinner command that calls the dragged and dropped Camera.
    [YarnCommand("focus_camera")]
    public void CameraLookAtTarget()
    {

        // Turns off Main Camera.
        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;

        // Turns on dragged and dropped Camera
        newCamera.GetComponent<Camera>().enabled = true;
    }

    // Reverts back to intial Camera being used.
    public void CameraDoneLoooking()

    {   // Turns off dragged and dropped Camera
        newCamera.GetComponent<Camera>().enabled = false;

        // Turns on Main Camera.
        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;     
    }
}