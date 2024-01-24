using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

 public enum EnemyStates
{
    Wandering,
    Hunting,
    Attacking,
    Fleeing,
    Dodging
}
public class EnemyAbstract: MonoBehaviour, IDamageable
{
    public EnemyStates EnemyState;
    [Range(5f, 300f)]
    public float enemyMaxHealth;
    public float enemyDefaultMovementSpeed;
    private float huntingvisionRange = 8f;
    public float AttackSpeed;
    public float currentEnemyHealth;
    private LayerMask playerLayer = 12;
    [Range(1,15)]
    public float Strength;
    private float enemyMaxAttackDmg;
    private float enemyMinAttackDmg;

    SpriteRenderer spriteRenderer;
    ParticleSystem enemyPS;
    ScoreManager score;
    HPBar healthBar;
    NavMeshAgent navMeshAgent;

    public void EnemyGetComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyPS = GetComponentInChildren<ParticleSystem>();
        score = FindObjectOfType<ScoreManager>();
        healthBar = GetComponentInChildren<HPBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public float CalculateEnemyAttackDmg(float dmgMultiplyer)
    {

        float damage = (((((2 * 2000)/ 5)+2)*Strength)/ 50)+2;
        if (dmgMultiplyer != 1)
            damage *= dmgMultiplyer;  
        return damage;
    }

     void IDamageable.Damage(float damageAmount)
    {
        currentEnemyHealth -= damageAmount;
        healthBar.UpdateHBar(currentEnemyHealth, enemyMaxHealth);
        EnemyVisualDamageTaken();
        
        if (enemyPS != null)       
            enemyPS.Play();
        if (currentEnemyHealth <= 0)
            EnemyDie();      
    }
    void EnemyDie()
    {
        score.AddToScore(1, 0.22f);
        Destroy(gameObject);
    }

    void EnemyVisualDamageTaken()
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
    IEnumerator EnemyFlash(SpriteRenderer spriteRenderer)
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

    public void UpdateEnemyState()
    {
        switch (EnemyState)
        {
            case EnemyStates.Wandering:
                StartCoroutine(WanderTimer());
                break;
            case EnemyStates.Attacking:
                //Attack player
                break;
            case EnemyStates.Hunting:
                //hunt player
                break;
            case EnemyStates.Fleeing:
                //stuff
                break;
            case EnemyStates.Dodging:
                //Stuff
                break;
        }
    }
    public void SetEnemyStates()
    {
        if (isAttacking())
        {
            EnemyState = EnemyStates.Attacking;
        }
    }
    private bool isHunting()
    {
       Physics2D.CircleCastAll(transform.position, huntingvisionRange, Vector2.zero, 1f, playerLayer);
     //  if ()
        return false;
    }
    private bool isAttacking()
    {
        //if in a small radius of the player
        return false;
    }
     public IEnumerator WanderTimer()
    {
        while (EnemyState == EnemyStates.Wandering)
        {
            yield return new WaitForSeconds(5f);
            SetRandomDestination();

            if (EnemyState != EnemyStates.Wandering)
                break;
        }
        
    }
    void SetRandomDestination()
    {
        float wandervisionRange = 5f;
        if (navMeshAgent != null)
        {

            Vector3 RndPosition = Random.insideUnitCircle * wandervisionRange;
            
            NavMeshHit hit;
            RaycastHit raycastHit;
            
            if (!Physics.Raycast(transform.position, RndPosition, out raycastHit, wandervisionRange))
            {
                if (NavMesh.SamplePosition(RndPosition + transform.position, out hit, wandervisionRange, 1))
                {
                    navMeshAgent.SetDestination(hit.position);
                }
            }

        }
        else
        {
            Debug.LogWarning("Navmesh Agent null on enemy: " + gameObject.name);
        }

    }
    bool IsCheckForObstacle(RaycastHit2D hit)
    {
        Vector2 hitPoint = hit.point;
        Vector2 hitNormal = hit.normal;
        
        // Cast a ray from the hit point in the direction of the hit normal
        RaycastHit2D obstacleHit = Physics2D.Raycast(hitPoint, hitNormal, huntingvisionRange, playerLayer);

        // Check if there's an obstacle in front
        if (obstacleHit.collider != null)
        {
            // Do something with the obstacle hit
            Debug.Log("Obstacle in front: " + obstacleHit.collider.gameObject.name);
            return true;
        }
        else
        {
            return false;
        }
    }

}
