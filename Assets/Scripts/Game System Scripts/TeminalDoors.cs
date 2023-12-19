using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeminalDoors : MonoBehaviour       // created by Devin Hunt
{
    public Sprite lockedTerminal;
    public Sprite unlockedTerminal;

    public GameObject correspondingDoor;

    private Animator doorAnimator;

    bool isDoor;
    bool isInteractable;

    float doorTimer = 1.0f;
    float currentTime;

    void Start()
    {
        currentTime = doorTimer;

        doorAnimator = correspondingDoor.GetComponent<Animator>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
     
        if (Input.GetKeyDown(KeyCode.E) && isInteractable == true)
        {
            if (isDoor == true && currentTime >= doorTimer) 
            {
                TerminalUnlocked();               
            }
            if (isDoor == false && currentTime >= doorTimer)
            {
                TerminalLocked();              
            }
        }            
    }

    void TerminalUnlocked()
    {
        isDoor = false;

        currentTime = 0.0f;

        doorAnimator.SetBool("doorStatus", true);
        
        gameObject.GetComponent<SpriteRenderer>().sprite = unlockedTerminal;
        
        correspondingDoor.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    void TerminalLocked()
    {   
        isDoor = true;

        currentTime = 0.0f;

        doorAnimator.SetBool("doorStatus", false);

        gameObject.GetComponent<SpriteRenderer>().sprite = lockedTerminal;

        correspondingDoor.GetComponent<CapsuleCollider2D>().enabled = true;      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {        
            isInteractable = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {         
            isInteractable = false;
        }
    }
}