using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn.Unity;

public class WhenDialogueIsRunning : MonoBehaviour
{
    private DialogueRunner dialogueRunner;

    GameObject player;
    GameObject enemy;
    Rigidbody2D rb;

    void Start()
    {
         
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
        rb = player.GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        Conversing();
    }

    void Conversing()
    {
        if (dialogueRunner.IsDialogueRunning == true)
        {
            if (player != null)
            {
                player.GetComponent<PlayerAttack>().enabled = false;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                PlayerAnimation playerAnimater = player.GetComponent<PlayerAnimation>();
                //set up a is paused bool to stop animation

            }
            if (enemy != null)
            {
                enemy.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
        else if (dialogueRunner.IsDialogueRunning == false)
        {
            if (player != null)
            {
                player.GetComponent<PlayerAttack>().enabled = true;
                rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;

            }
            if (enemy != null)
            {
                enemy.GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }

}
