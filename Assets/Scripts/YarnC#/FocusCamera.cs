using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;
using Yarn.Unity;

public class FocusCamera : MonoBehaviour 
{

    // Drag and drop your Dialogue Runner into this variable.
    public DialogueRunner dialogueRunner;
    public GameObject newCamera;

    public void Awake()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
       
        dialogueRunner.onDialogueComplete.AddListener(CameraDoneLoooking);
    }
    
    public void Start()
    {
       
    }

    [YarnCommand("focus_camera")]
    public void CameraLookAtTarget()
    {

        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
        newCamera.GetComponent<Camera>().enabled = true;
    }

    public void CameraDoneLoooking()
    {     
        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
        newCamera.GetComponent<Camera>().enabled = false;
    }
}