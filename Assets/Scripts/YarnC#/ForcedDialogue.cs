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
    float upTownPass;
    float loveLetter;
    public GameObject Snork;
    public GameObject LadyDow;
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

    public void OnTriggerEnter2D()
    {
        canTalk = true;
    }

    public void OnTriggerExit2D()
    {
        canTalk = false;
    }

    void Update()
    {
      variableStorage.TryGetValue<float>("$Uptown_Pass", out upTownPass);
      variableStorage.TryGetValue<float>("$Love_Letter", out loveLetter);

        if (canTalk) 
        {
            StartConversation();

            GameObject.Find("Player").GetComponent<Transform>().Translate(0.0f, -0.3f, 0.0f);
            
            if (upTownPass == 1)
            {
                GameObject.Find("Guardus").GetComponent<BoxCollider2D>().enabled = false;
            }

        }

        if (loveLetter == 3) 
        {
            Destroy(Snork);
            
        }
        if (loveLetter == 4) 
        {
            GameObject.Find("Snork").GetComponent<Transform>().transform.position = LadyDow.transform.position + new Vector3(-0.75f, 0f, 0f);
        }

    }
}

