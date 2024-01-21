using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAbstract: MonoBehaviour , IDamageable
{
    [Range(5f, 300f)]
    public float enemyMaxHealth;
    public float enemyDefaultMovementSpeed;
    public float Strength;
    public float AttackSpeed;
    public float currentEnemyHealth;

    private float enemyMaxAttackDmg;
    private float enemyMinAttackDmg;

    SpriteRenderer spriteRenderer;
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
     void IDamageable.Damage(float damageAmount)
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
    public void EnemyDie()
    {
        score.IncrementScore();
        Destroy(gameObject);
    }

    public void EnemyVisualDamageTaken()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

        if (spriteRenderer == null)
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
