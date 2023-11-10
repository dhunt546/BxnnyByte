using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackCooldown = 2.0f;
    public bool canAttack = true;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canAttack)
        {
            // Enemy attacks
            Debug.Log("Enemy attacks!");

            // Set cooldown
            canAttack = false;
            Invoke("ResetAttackCooldown", attackCooldown);
        
        }
    }

    public void ResetAttackCooldown()
    {
        canAttack = true;
    }
}
