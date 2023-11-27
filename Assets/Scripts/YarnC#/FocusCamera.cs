using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;
using Yarn.Unity;

public class FocusCamera : MonoBehaviour
{
   
    DialogueRunner dialogueRunner;
    public GameObject newCamera;
   

    public void Awake()
    {   
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();     
    
        dialogueRunner.onDialogueComplete.AddListener(CameraDoneLoooking);
    }


    public void Start()
    {
        newCamera.GetComponent<Camera>().enabled = false;
    }
    
  
    [YarnCommand("focusCamera")]
    public void focusCamera()
    {       
        newCamera.GetComponent<Camera>().enabled = true;                
    }

    [YarnCommand("doneFocus")]
    public void doneFocus()
    {         
            newCamera.GetComponent<Camera>().enabled = false;    
    }
   
    public void CameraDoneLoooking()
    {
        newCamera.GetComponent<Camera>().enabled = false;           
    }
}