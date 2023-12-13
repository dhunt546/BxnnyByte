using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float maxEnemyHealth = 1.0f;
    [SerializeField] HPBar healthBar;
    public ScoreManager score;

    private float currentEnemyHealth;


    private void Start()
    {
        score = score.GetComponent<ScoreManager>();
        currentEnemyHealth = maxEnemyHealth;
        healthBar = GetComponentInChildren<HPBar>();
    }
    void IDamageable.Damage(float damageAmount)
    {
        currentEnemyHealth -= damageAmount;

        healthBar.UpdateHBar(currentEnemyHealth, maxEnemyHealth);

            if (currentEnemyHealth <= 0)
            {
            
                Die();
            }
    }

    private void Die()
    {
        score.IncrementScore();
        Destroy(gameObject);      
    }
}
