using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float enemyAttackCooldown = 2.0f;
    private bool canAttack;

    public void Start()
    {
         
         canAttack =true;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canAttack)
        {
            // Enemy attacks
            Debug.Log("Enemy attacks!");

            // Set cooldown
            canAttack = false;
            Invoke("ResetAttackCooldown", enemyAttackCooldown);
        
        }
    }

    public void ResetAttackCooldown()
    {
        canAttack = true;
    }
}
