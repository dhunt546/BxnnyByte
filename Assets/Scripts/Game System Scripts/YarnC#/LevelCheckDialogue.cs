using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Yarn;
using Yarn.Unity;

public class LevelCheckDialogue : MonoBehaviour
{   
    DialogueRunner dialogueRunner;

    public string conversationStartNode;

    bool isCurrentConversation = false;

    int enemyCount;
    int debrisCount;

    public int enemyRequiredPass;
    public int debrisRequiredPass;
    public int enemyRequiredFail;
    public int debrisRequiredFail;

    private void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();

        dialogueRunner.onDialogueComplete.AddListener(EndConversation);

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        debrisCount = GameObject.FindGameObjectsWithTag("Debris").Length;
    }

    private void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        debrisCount = GameObject.FindGameObjectsWithTag("Debris").Length;
    }
    private void StartConversation()
    {
        if (isCurrentConversation == false)
        {
            isCurrentConversation = true;

            dialogueRunner.StartDialogue(conversationStartNode);

            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;

            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && enemyCount >= enemyRequiredFail && debrisCount >= debrisRequiredFail)
        {
            StartConversation();         
        }
        if (other.CompareTag("Player") && enemyCount <= enemyRequiredPass && debrisCount <= debrisRequiredPass)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}