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
    public Vector3 moveDirection = Vector3.forward;
    public string conversationStartNode;
    bool isCurrentConversation = false;

    int debrisLeftInArea;
    int enemiesLeftInArea;

    private void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();

        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }
    private void StartConversation()
    {
        if (isCurrentConversation == false)
        {
            isCurrentConversation = true;

            dialogueRunner.StartDialogue(conversationStartNode);

            GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
        }
    }

    private void EndConversation()
    {
        if (isCurrentConversation) 
        {
            isCurrentConversation = false;

            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
        }
    }


    void CheckForEnemies()
    {
        Collider2D collider = GetComponent<PolygonCollider2D>();

        if (collider != null)
        {
            List<Collider2D> overlappingColliders = new List<Collider2D>();

            int count = collider.OverlapCollider(new ContactFilter2D(), overlappingColliders);
            enemiesLeftInArea = 0;

            for (int i = 0; i < count; i++)
            {
                if (overlappingColliders[i].CompareTag("Enemy"))
                        enemiesLeftInArea++;
                
            }
        }
    }
    
    void CheckForDebris()
    {

        HashSet<Debris> uniqueDebrisSet = new HashSet<Debris>();
        Collider2D collider = GetComponent<PolygonCollider2D>();
        //Debug.Log(collider.name);
        if (collider != null)
        {
            List<Collider2D> overlappingColliders = new List<Collider2D>();
            int count = collider.OverlapCollider(new ContactFilter2D(), overlappingColliders);


            for (int i = 0; i < count; i++)
            {
                // Check if the current collider has a specific script attached.
                 Debris debrisScript = overlappingColliders[i].GetComponent<Debris>();
                //Debug.Log("collider " + overlappingColliders[i].name);
                if (debrisScript != null)
                {
                    // Increment the count if the specific script is found.
                    uniqueDebrisSet.Add(debrisScript);

                    Debug.Log("Debris left" + uniqueDebrisSet);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckForDebris();
        CheckForEnemies();
        
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition + moveDirection;
        if (other.CompareTag("Player") && enemiesLeftInArea == 0 && debrisLeftInArea == 0)
        {
            StartConversation();
            other.gameObject.transform.position = newPosition;
        }
    }

}