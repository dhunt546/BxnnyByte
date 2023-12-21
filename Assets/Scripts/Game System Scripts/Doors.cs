using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public GameObject doorPrefab;

    private Animator doorAnimator;
 
    void Start()
    {
        doorAnimator = GetComponent<Animator>();        
    }
    public void OnDoorOpened()
    {
       
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {                                  
            doorAnimator.SetBool("doorStatus", true);
               
            doorPrefab.GetComponent<CapsuleCollider2D>().enabled = false;            
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            doorAnimator.SetBool("doorStatus", true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {                                
            doorAnimator.SetBool("doorStatus", false);
             
            doorPrefab.GetComponent<CapsuleCollider2D>().enabled = true;           
        }
    }
}
