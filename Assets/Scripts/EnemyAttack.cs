using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour  //Jacob foran Enemy Attack
{
    public float attackDamage = 1.0f;
    public float enemyAttackCooldown = 1.0f; 

    private float attackTimer = 0.0f;
    private bool isPlayerInRange = false;

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

    private void AttackPlayer()
    {
        // Check if the player is still in range before attacking
        if (isPlayerInRange)
        {
            // Add attack logic here
            Debug.Log("Enemy attacks player!");
            
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
