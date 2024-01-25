using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


 public enum EnemyStates
{
    Wandering,
    Hunting,
    Seeking,
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

    private float huntingvisionRange = 15f;
    private bool isWandering = false;

    public float AttackSpeed;
    public float currentEnemyHealth;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask Walls;
    [Range(1,15)]
    public float Strength;
    private float enemyMaxAttackDmg;
    private float enemyMinAttackDmg;

    GameObject player;
    SpriteRenderer spriteRenderer;
    ParticleSystem enemyPS;
    ScoreManager score;
    HPBar healthBar;
    NavMeshAgent navMeshAgent;
    Rigidbody2D enemyRb;

    public void EnemyGetComponents()
    {
        currentEnemyHealth = enemyMaxHealth;
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyPS = GetComponentInChildren<ParticleSystem>();
        score = FindObjectOfType<ScoreManager>();
        healthBar = GetComponentInChildren<HPBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyRb = GetComponent<Rigidbody2D>();
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
            EnemyKnockBack();
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
      Color originalColor = spriteRenderer.color;

        if (spriteRenderer != null && spriteRenderer.gameObject != null && spriteRenderer.gameObject.activeSelf)
        {
            spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(flashDuration);
            if (spriteRenderer != null && spriteRenderer.gameObject != null && spriteRenderer.gameObject.activeSelf)
            { spriteRenderer.color = new Color(1f, 1f, 1f);}    
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
        else if (IsSeeking())
        {
            EnemyState = EnemyStates.Seeking;
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
                Hunting();
                break;
            case EnemyStates.Seeking:
                StartCoroutine(Seeking());
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
    private bool wasSeen;
    private bool IsHunting()
    {
       if (Physics2D.CircleCast(transform.position, huntingvisionRange, Vector2.zero, huntingvisionRange, playerLayer))
        {
            if (!IsCheckForObstacle(Physics2D.CircleCast(transform.position, huntingvisionRange, Vector2.zero, huntingvisionRange, playerLayer)))
            {   
                wasSeen = true;
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
    bool IsSeeking()
    {
        if (wasSeen && !IsHunting())
        {      
            return true;
        }
        return false;
    }
    IEnumerator Seeking()
    {       
        navMeshAgent.SetDestination(player.transform.position);
        yield return new WaitForSeconds(12f);
        if (!Physics2D.CircleCast(transform.position, huntingvisionRange, Vector2.zero, 8f, playerLayer))
        wasSeen = false;
    }
    void Hunting()
    {
        StopCoroutine(WanderTimer());
        RaycastHit2D PlayerLastHit = Physics2D.CircleCast(transform.position, huntingvisionRange, Vector2.zero, huntingvisionRange, playerLayer);
        navMeshAgent.SetDestination(PlayerLastHit.transform.position);
        wasSeen = true;

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
    private Vector3 storedHitPoint;
    private RaycastHit2D storedObstacleHit;
    bool IsCheckForObstacle(RaycastHit2D hit)
    {
        Vector3 hitPoint = hit.point;
        float distance = Vector2.Distance(transform.position, hitPoint);

        // Cast a ray from the hit point in the direction of the hit normal
        RaycastHit2D obstacleHit = Physics2D.Raycast(transform.position, hitPoint - transform.position, distance, Walls);

        storedHitPoint = hitPoint;
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

        // Draw a line from transform.position to the storedHitPoint
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, storedHitPoint);

        // Draw the result of the stored obstacle raycast
        if (storedObstacleHit.collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(storedObstacleHit.point, storedObstacleHit.point + storedObstacleHit.normal * 0.1f); // Draw a small line along the normal at the hit point
            Gizmos.DrawWireSphere(storedObstacleHit.point, 0.1f); // Draw a small sphere at the hit point
        }
    }

    public float enemyThrust;
    public float enemyKnocktime;
    public virtual void EnemyKnockBack()
    {
        if (enemyRb != null)
        {
            enemyRb.isKinematic = false;
            Vector2 difference = transform.position - player.transform.position;
            difference = difference.normalized * enemyThrust;
            enemyRb.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockCo());
        }
    }
    IEnumerator KnockCo()
    {
        if (enemyRb != null)
        {
            yield return new WaitForSeconds(enemyKnocktime);
            enemyRb.velocity = Vector2.zero;
            enemyRb.isKinematic = true;
        }
    }
}
