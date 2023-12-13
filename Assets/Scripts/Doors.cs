using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public GameObject doorPrefab;

    private Animator doorAnimator;
 
    bool doorStatus = false;

    void Start()
    {
        doorAnimator = GetComponent<Animator>();        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {                        
            if(doorStatus == false)
            {
                doorAnimator.SetBool("doorStatus", true);
                doorStatus = true;
                doorPrefab.GetComponent<EdgeCollider2D>().enabled = false;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {                     
            if (doorStatus == true)
            {
                doorAnimator.SetBool("doorStatus", false);
                doorStatus = false;
                doorPrefab.GetComponent<EdgeCollider2D>().enabled = true;
            }
        }
    }
}
