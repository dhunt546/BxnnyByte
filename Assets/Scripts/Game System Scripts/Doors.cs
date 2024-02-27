using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Doors : MonoBehaviour
{
    public GameObject doorPrefab;

    private Animator doorAnimator;

    private AudioSource openingSound;
 
    void Start()
    {
        doorAnimator = GetComponent<Animator>();   
        openingSound = GetComponent<AudioSource>();
    }
    public void OnDoorOpened()
    {
       
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if (doorAnimator != null)
            {
                openingSound.Play();

                doorAnimator.SetBool("doorStatus", true);

                doorPrefab.GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetBool("doorStatus", true);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetBool("doorStatus", false);

                doorPrefab.GetComponent<CapsuleCollider2D>().enabled = true;
            }
        }
    }
}
