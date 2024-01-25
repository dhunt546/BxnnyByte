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
    private bool isWandering = false;

    public float AttackSpeed;
    public float currentEnemyHealth;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask Walls;
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

    public void EnemyUpdate()
    {
        SetEnemyStates();
        UpdateEnemyState();
    }

    void SetEnemyStates()
    {
        if (IsAttacking())
        {
            EnemyState = EnemyStates.Attacking;
        }
        else if (IsHunting())
        {
            EnemyState = EnemyStates.Hunting;
        }
        else
        {
            EnemyState = EnemyStates.Wandering;
        }
    }
    public void UpdateEnemyState()
    {
        switch (EnemyState)
        {    
            case EnemyStates.Attacking:
                //Attack player
                break;
            case EnemyStates.Hunting:
                Debug.Log("Enemy Hunting state");
                //hunt player
                break;
            case EnemyStates.Fleeing:
                //stuff
                break;
            case EnemyStates.Dodging:
                //Stuff
                break;
            case EnemyStates.Wandering:
                if (!isWandering)
                {
                    StartCoroutine(WanderTimer());
                }
                break;
        }
    }

    private bool IsHunting()
    {
       if (Physics2D.CircleCast(transform.position, huntingvisionRange, Vector2.zero, huntingvisionRange, playerLayer))
        {
            if (!IsCheckForObstacle(Physics2D.CircleCast(transform.position, huntingvisionRange, Vector2.zero, huntingvisionRange, playerLayer)))
            {
                Debug.Log("is hunting true");
                return true;
            }
            else
            {
                return false;
                //obstacle in front of player (cant see)
            }
        }
        return false;
    }
    private bool IsAttacking()
    {
        if (Physics2D.CircleCast(transform.position, 2f, Vector2.zero, 2f, playerLayer))
        {
            return true;
        } 
        else
        return false;
    }

    void Hunting()
    {
        navMeshAgent.SetDestination();
    }
    public IEnumerator WanderTimer()
    {
        Debug.Log("Start Wander Time");
        isWandering = true;
        while (EnemyState == EnemyStates.Wandering)
        {
            yield return new WaitForSeconds(5f);
            SetRandomDestination();

            if (EnemyState != EnemyStates.Wandering)
            {
                isWandering = false;
                break;
            }

               
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

    //This is only for gizmos visuals
    private Vector2 storedHitPoint;
    private Vector2 storedHitNormal;
    private RaycastHit2D storedObstacleHit;
    bool IsCheckForObstacle(RaycastHit2D hit)
    {
        Vector2 hitPoint = hit.point;
        Vector2 hitNormal = hit.normal;
        
        // Cast a ray from the hit point in the direction of the hit normal
        RaycastHit2D obstacleHit = Physics2D.Raycast(hitPoint, hitNormal, huntingvisionRange, Walls);

        storedHitPoint = hitPoint;
        storedHitNormal = hitNormal;
        storedObstacleHit = obstacleHit;

        // Check if there's an obstacle in front
        if (obstacleHit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void OnDrawGizmosSelected()
    {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, huntingvisionRange);

        
        // Draw the ray from the stored hit point in the direction of the stored hit normal
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(storedHitPoint, storedHitPoint + storedHitNormal * huntingvisionRange);

        // Draw the result of the stored obstacle raycast
        if (storedObstacleHit.collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, storedObstacleHit.point);
            Gizmos.DrawWireSphere(storedObstacleHit.point, 0.1f); // Draw a small sphere at the hit point
        }
    }
}
