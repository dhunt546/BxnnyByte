using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    EnemyAnimator animatorScript;
    public float maxEnemyHealth = 1.0f;
    ParticleSystem enemyPS;
    [SerializeField] HPBar healthBar;


    ScoreManager score;

    private float currentEnemyHealth;

    private void Start()
    {
        enemyPS = GetComponentInChildren<ParticleSystem>();
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
        if (enemyPS != null)
        {
            enemyPS.Play();
        }

        if (currentEnemyHealth <= 0)
            {            
                Die();
            }
    }

    void Die()
    {
        score.AddToScore(1, 0.3f);
        Destroy(gameObject);      
    }
}
