using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteract : MonoBehaviour
{

    DialogueRunner dialogueRunner;
    
    public string conversationStartNode;

    bool isCurrentConversation = false;
    bool canTalk = false;

    public void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();

        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }
  
    public void StartConversation()
    {
        if (isCurrentConversation == false)
        {
            isCurrentConversation = true;

            dialogueRunner.StartDialogue(conversationStartNode);       
        }
    }

    public void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
         
        }
    }
       

    public void OnTriggerEnter2D()
    {
        canTalk = true;
    }

    public void OnTriggerExit2D()
    {
        canTalk = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canTalk)
            
                StartConversation();
        }
    }
}
    
