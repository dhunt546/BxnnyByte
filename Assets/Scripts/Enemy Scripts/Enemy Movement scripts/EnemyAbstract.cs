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
    private bool isAttacking = false;

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
    HPBar healthBar;
    NavMeshAgent navMeshAgent;
    Rigidbody2D enemyRb;

    public void EnemyGetComponents()
    {
        currentEnemyHealth = enemyMaxHealth;
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyPS = GetComponentInChildren<ParticleSystem>();
        healthBar = GetComponentInChildren<HPBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyRb = GetComponent<Rigidbody2D>();

        SetSteeringOrigins();
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
        ScoreManager.AddToScore(1, 0.22f);
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
        if (IsInAttackingRange())
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
                if (!isAttacking) 
                EnemyAttacking();
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
    private float originalSpeed, originalAcc, originalStoppingDistance;
    [SerializeField] private float circlingSpeed, newSpeed, newAcc;
    
    private bool IsInAttackingRange()
    {
        if (Physics2D.CircleCast(transform.position, 2f, Vector2.zero, 2f, playerLayer))
        {      
            return true;
        } 
        else
        return false;
    }
    void SetSteeringOrigins()
    {
        originalSpeed = navMeshAgent.speed;
        originalAcc = navMeshAgent.acceleration;
        originalStoppingDistance = navMeshAgent.stoppingDistance;
    }
    void ResetState()
    {
        StopCoroutine(WanderTimer());
        StopCoroutine(CirclePlayer());
        navMeshAgent.speed = originalSpeed;
        navMeshAgent.acceleration = originalAcc;
        navMeshAgent.stoppingDistance = originalStoppingDistance;
    }
    void EnemyAttacking()
    {
        ResetState();
        isAttacking = true;
        StartCoroutine(CirclePlayer());
    }
    public float circleRadius;
    float currentcirclingSpeed;
    IEnumerator CirclePlayer()
    {  
        Vector3 enemyInitialPosition = transform.position;
        Vector3 playerInitialPosition = player.transform.position;

        //Flips Direction of cirlcing
        bool circleDirection = enemyInitialPosition.x <= playerInitialPosition.x && enemyInitialPosition.y >= playerInitialPosition.y;    
        if (circleDirection)      
        { currentcirclingSpeed = -Mathf.Abs(circlingSpeed); }
        else if (!circleDirection)
        { currentcirclingSpeed = circlingSpeed;}


        navMeshAgent.speed = newSpeed; navMeshAgent.acceleration = newAcc;
        navMeshAgent.stoppingDistance = 0f;
        while (isAttacking) 
        {       
            Vector3 playerPosition = player.transform.position;
            float angle = Time.time * currentcirclingSpeed;
            Vector3 circlePosition = playerPosition + new Vector3(Mathf.Cos(angle) * circleRadius, Mathf.Sin(angle) * circleRadius, 0f);
            if (navMeshAgent.enabled)
            navMeshAgent.SetDestination(circlePosition);
            yield return null;
        }
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
        ResetState();
        if (navMeshAgent.enabled)
            navMeshAgent.SetDestination(player.transform.position);

        yield return new WaitForSeconds(12f);
        if (!Physics2D.CircleCast(transform.position, huntingvisionRange, Vector2.zero, 8f, playerLayer))
        wasSeen = false;
    }
    
    void Hunting()
    {
        ResetState();
        isAttacking = false;
        RaycastHit2D PlayerLastHit = Physics2D.CircleCast(transform.position, huntingvisionRange, Vector2.zero, huntingvisionRange, playerLayer);
       if (navMeshAgent.enabled)
        navMeshAgent.SetDestination(PlayerLastHit.transform.position);
        wasSeen = true;

    }
    public IEnumerator WanderTimer()
    {
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
                    if (navMeshAgent.enabled)
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
