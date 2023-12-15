using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Yarn;
using Yarn.Unity;

public class ForcedDialogue : MonoBehaviour
{

    DialogueRunner dialogueRunner;

    public string conversationStartNode;
    bool isCurrentConversation = false;
    bool canTalk = false;

    private InMemoryVariableStorage variableStorage;


    public void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();

        dialogueRunner.onDialogueComplete.AddListener(EndConversation);

        variableStorage = FindObjectOfType<InMemoryVariableStorage>();
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
        if (other.CompareTag("Player"))
        {
            StartConversation();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

    }
}

