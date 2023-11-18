using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float maxEnemyHealth = 10.0f;
    [SerializeField] HPBar healthBar;

    private float currentEnemyHealth;


    private void Start()
    {
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
        Destroy(gameObject);
    }
}
