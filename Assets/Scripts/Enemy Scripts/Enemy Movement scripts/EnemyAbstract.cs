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

    public float AttackSpeed;
    public float currentEnemyHealth;

    [Range(1,15)]
    public float Strength;
    private float enemyMaxAttackDmg;
    private float enemyMinAttackDmg;

    SpriteRenderer spriteRenderer;
    ParticleSystem enemyPS;
    EnemyAnimator animatorScript;
    ScoreManager score;
    HPBar healthBar;

    public void EnemyGetComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyPS = GetComponentInChildren<ParticleSystem>();
        animatorScript = GetComponentInChildren<EnemyAnimator>();
        score = FindObjectOfType<ScoreManager>();
        healthBar = GetComponentInChildren<HPBar>();
    }

    public float CalculateEnemyAttackDmg(int maxAttackDmg, int minAttackDmg, float dmgMultiplyer)
    {
        float damage = Random.Range(minAttackDmg, maxAttackDmg);
        if (dmgMultiplyer != 1)
            damage *= dmgMultiplyer;  
        return damage;
    }

     void IDamageable.Damage(float damageAmount)
    {
        currentEnemyHealth -= damageAmount;

        healthBar.UpdateHBar(currentEnemyHealth, enemyMaxHealth);
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
            EnemyDie();
        }
    }
    public void EnemyDie()
    {
        score.IncrementScore();
        Destroy(gameObject);
    }

    public void EnemyVisualDamageTaken()
    {
        if (spriteRenderer != null)
        {
            StartCoroutine(EnemyFlash(spriteRenderer));
        }
        else
        {
            Debug.LogError("SpriteRenderer is null. Make sure the object has a SpriteRenderer component.");
        }
    }
    public IEnumerator EnemyFlash(SpriteRenderer spriteRenderer)
    {

        float flashDuration = 0.2f;

        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;

            // Flash red
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
            Debug.Log(spriteRenderer);

        }
    }

}
