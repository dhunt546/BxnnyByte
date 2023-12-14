using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class WhenDialogueIsRunning : MonoBehaviour
{
    DialogueRunner dialogueRunner;

    GameObject player;
    GameObject enemy;

    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
    }

  
    void Update()
    {
        Conversing();
    }

    void Conversing()
    {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");

        if (dialogueRunner.IsDialogueRunning == true)
        {
            if (player != null)
            {
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<PlayerAttack>().enabled = false;
            }
            if (enemy != null)
            {
                enemy.GetComponent<EnemyAI>().enabled = false;
            }
        }
        else if (dialogueRunner.IsDialogueRunning == false)
        {
            if (player != null)
            {
                player.GetComponent<PlayerMovement>().enabled = true;
                player.GetComponent<PlayerAttack>().enabled = false;
            }
            if (enemy != null)
            {
                enemy.GetComponent<EnemyAI>().enabled = true;
            }
        }
    }

}
