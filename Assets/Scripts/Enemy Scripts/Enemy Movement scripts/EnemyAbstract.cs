using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class EnemyAbstract: MonoBehaviour , IDamageable
{
    [Range(5f, 300f)]
    public float enemyMaxHealth;
    public float enemyDefaultMovementSpeed;
    public float Strength;
    public float AttackSpeed;

    private float enemyMaxAttackDmg;
    private float enemyMinAttackDmg;


    ParticleSystem enemyPS;
    EnemyAnimator animatorScript;
    ScoreManager score;

    public float CalculateEnemyAttackDmg(int maxAttackDmg, int minAttackDmg, float dmgMultiplyer)
    {
        float damage = Random.Range(minAttackDmg, maxAttackDmg);
        if (dmgMultiplyer != 1)
            damage *= dmgMultiplyer;  
        return damage;
    }

    public Slider enemyHBar;
    public void UpdateHBar(float currentEnemyHealth, float maxEnemyHealth)
    {
        if (enemyHBar != null)
        {
            enemyHBar.value = currentEnemyHealth / maxEnemyHealth;
        }
        else
        {
            Debug.Log("HPBar slider not found");
        }
    }
    public void IDamageable.Damage(float currentEnemyHealth, float damageAmount)
    {
        currentEnemyHealth -= damageAmount;

        UpdateHBar(currentEnemyHealth, enemyMaxHealth);
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
        score.IncrementScore();
        Destroy(gameObject);
    }



}
