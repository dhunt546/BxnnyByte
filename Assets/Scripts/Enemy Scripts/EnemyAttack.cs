using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class EnemyAttack : MonoBehaviour  //Jacob foran Enemy Attack
{
    public float attackDamage = 1.0f;
    public float enemyAttackCooldown = 1.0f; 

    private float attackTimer = 0.0f;
    private bool isPlayerInRange = false;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void FixedUpdate()
    {
        if (isPlayerInRange)
        {
            // Timer to control attack intervals
            attackTimer += Time.fixedDeltaTime;
            if (attackTimer >= enemyAttackCooldown)
            {
                AttackPlayer();
                attackTimer = 0.0f;                
            }
        }
    }
    public void AttackPlayer()
    {
        
        
        if (isPlayerInRange && playerController != null)
        {
            // Damage the player by calling the TakeDamage method from HealthBar script
            playerController.TakeDamage(attackDamage);
            

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            attackTimer = 0.0f; // Reset attack timer when player exits range
        }
    }
}
