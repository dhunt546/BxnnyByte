using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Yarn;
using Yarn.Unity;

public class LevelCheckDialogue : MonoBehaviour         // created by Devin Hunt
{   
    DialogueRunner dialogueRunner;

    public string conversationStartNode;

    bool isCurrentConversation = false;

    public int enemyRequiredPass;
    public int debrisRequiredPass;
    public int enemyRequiredFail;
    public int debrisRequiredFail;

    public void Awake()
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

            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        }
    }

    public void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;

            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && CleanUpObjectiveManager.enemyCount >= enemyRequiredFail 
                                       && CleanUpObjectiveManager.debrisCount >= debrisRequiredFail)
        {
            StartConversation();         
        }
        if (other.CompareTag("Player") && CleanUpObjectiveManager.enemyCount <= enemyRequiredPass 
                                       && CleanUpObjectiveManager.debrisCount <= debrisRequiredPass)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}