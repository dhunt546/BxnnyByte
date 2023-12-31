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
    private HealthBar playerHealthBar;

    private void Start()
    {
        playerHealthBar = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBar>();
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
        Debug.Log("Enemy attacks player!");
        
        if (isPlayerInRange && playerHealthBar != null)
        {
            // Damage the player by calling the TakeDamage method from HealthBar script
            playerHealthBar.TakeDamage(attackDamage);
            Debug.Log(":::::::::::");

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
