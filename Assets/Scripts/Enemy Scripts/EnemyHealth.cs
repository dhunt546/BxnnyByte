using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    EnemyAnimator animatorScript;
    public float maxEnemyHealth = 1.0f;
   
    [SerializeField] HPBar healthBar;

   
    ScoreManager score;

    private float currentEnemyHealth;

    private void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
        score = FindObjectOfType<ScoreManager>();
        currentEnemyHealth = maxEnemyHealth;
        healthBar = GetComponentInChildren<HPBar>();
        animatorScript = GetComponent<EnemyAnimator>();
    }

    void IDamageable.Damage(float damageAmount)
    {
        currentEnemyHealth -= damageAmount;
        
        healthBar.UpdateHBar(currentEnemyHealth, maxEnemyHealth);
        if (animatorScript != null)
        {
            animatorScript.EnemyVisualDamageTaken();
        }

        if (currentEnemyHealth <= 0)
            {            
                Die();
            }
    }

    void Die()
    {
        score.IncrementScore();
        Destroy(gameObject);      
    }
}
