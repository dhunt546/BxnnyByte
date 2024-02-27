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
    [Header("Enemy States")]
    public EnemyStates EnemyState;
    [Range(5f, 300f)]
    public float enemyMaxHealth;
    [Range(2f,20f)]
    public float enemyDefaultMovementSpeed;

    private float huntingvisionRange = 15f;
    public float attackRange;

    private bool isWandering = false;
    private bool isStartAttack = false;

    private bool isAttacking = false;
    private bool canAttack = true;

    private float originalSpeed, originalAcc, originalStoppingDistance;
    [SerializeField] private float circlingSpeed, newSpeed, newAcc;

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
    public Rigidbody2D enemyRb;

    public void EnemyGetComponents()
    {
        currentEnemyHealth = enemyMaxHealth;
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyPS = GetComponentInChildren<ParticleSystem>();
        healthBar = GetComponentInChildren<HPBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyRb = GetComponent<Rigidbody2D>();
        enemyDefaultMovementSpeed = navMeshAgent.speed;
        SetSteeringOrigins();
        
    }
    public void EnemyUpdate()
    {
        SetEnemyStates();
        UpdateEnemyState();
        if (isEnemyBehindPlayer())
        {
            spriteRenderer.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        else
            spriteRenderer.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
    }

    bool isEnemyBehindPlayer()
    {
        if(transform.position.y >= player.transform.position.y) 
            return false;
        else
            return true;
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
      //BROKENNNN :((((
      //Color originalColor = spriteRenderer.color;

        if (spriteRenderer != null && spriteRenderer.gameObject != null && spriteRenderer.gameObject.activeSelf)
        {
            spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(flashDuration);
            if (spriteRenderer != null && spriteRenderer.gameObject != null && spriteRenderer.gameObject.activeSelf)
            { spriteRenderer.color = new Color(1f, 1f, 1f);}    
        }
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
                if (!isStartAttack) 
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
        StopCoroutine(AttackPlayer());
        ResetNavMesh();
    }
    void ResetNavMesh()
    {
        navMeshAgent.speed = originalSpeed;
        navMeshAgent.acceleration = originalAcc;
        navMeshAgent.stoppingDistance = originalStoppingDistance;
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
        isStartAttack = false;
        if (navMeshAgent.enabled)
            navMeshAgent.SetDestination(player.transform.position);

        yield return new WaitForSeconds(12f);
        if (!Physics2D.CircleCast(transform.position, huntingvisionRange, Vector2.zero, 8f, playerLayer))
        wasSeen = false;
    }
    
    void Hunting()
    {
        ResetState();
        isStartAttack = false;

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
    public float hitboxSize = 1f;
   public Vector3 HitboxCenter;
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

        //AttackHitBox
        Gizmos.color = Color.magenta;
        Vector2 hitboxCenter = transform.position + HitboxCenter;
        Gizmos.DrawWireSphere(hitboxCenter, hitboxSize);
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

    //Enemy Attack:

    private bool isCirclingPlayer()
    {
        if (EnemyState != EnemyStates.Attacking)
        {
            return false;
        }
        else
        {
            if (isAttacking)
                return false;
            else
            return true;
        }

    }
    void EnemyAttacking()
    {
        ResetState();
        isStartAttack = true;    
        StartCoroutine(CirclePlayer());
        StartCoroutine(AttackPlayer());
    }
    public float circleRadius;
    float currentcirclingSpeed;
    IEnumerator CirclePlayer()
    {
        while (EnemyState == EnemyStates.Attacking)
        {
            Vector3 enemyInitialPosition = transform.position;
            Vector3 playerInitialPosition = player.transform.position;

            //Flips Direction of cirlcing
            bool circleDirection = enemyInitialPosition.x <= playerInitialPosition.x && enemyInitialPosition.y >= playerInitialPosition.y;

            if (circleDirection)
            { currentcirclingSpeed = -Mathf.Abs(circlingSpeed); }
            else if (!circleDirection)
            { currentcirclingSpeed = circlingSpeed; }

            while (isCirclingPlayer())
            {
                navMeshAgent.speed = newSpeed; navMeshAgent.acceleration = newAcc;
                navMeshAgent.stoppingDistance = 0f;
                Vector3 playerPosition = player.transform.position;
                float angle = Time.time * currentcirclingSpeed;
                Vector3 circlePosition = playerPosition + new Vector3(Mathf.Cos(angle) * circleRadius, Mathf.Sin(angle) * circleRadius, 0f);
                if (navMeshAgent.enabled)
                {
                    navMeshAgent.SetDestination(circlePosition);
                }
                yield return null;
            }
            yield return null;
        }
        
    }

    public float enemyAttackCooldown;
    IEnumerator AttackEnemyCooldown()
    {
        ResetNavMesh();
        canAttack = false;
        isAttacking = false;
        yield return new WaitForSeconds(enemyAttackCooldown);
        canAttack = true;
    }
  
    private bool IsInAttackingRange()
    {
        if (Physics2D.CircleCast(transform.position, attackRange, Vector2.zero, attackRange, playerLayer))
        {
            return true;
        }
        else
            return false;
    }
    void SetJumpingNumbers()
    {
        navMeshAgent.speed = 90f;
        navMeshAgent.acceleration = 60f;
        navMeshAgent.stoppingDistance = 0f;
    }
    private Vector3 previousPosition;
    public string enemyDirection()
    {
        string stringDirection;
        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // Calculate the direction by subtracting the current position from the previous position
        Vector3 direction = currentPosition - previousPosition;

            direction.Normalize();


        previousPosition = currentPosition;
    
        //Set Direction of player if they have moved.
        if (direction.y >= 0.1)
            stringDirection = "Right";
        else if (direction.x <= -0.1)
            stringDirection = "Backward";
        else if (direction.y <= -0.1)
            stringDirection = "Left";
        else
            stringDirection = "Forward";

        return stringDirection;
    }

    IEnumerator AttackPlayer()
    {
        while (EnemyState == EnemyStates.Attacking) {
            //grabs player position a bit before it attacks so its not 100% accurate
            Vector3 PlayerDirection = player.transform.position;
            yield return new WaitForSeconds(0.4f);

            if (canAttack)
            {
                isAttacking = true;

            }

            if (isAttacking)
            {
                //sets steering numbers so it jumps rly fast

                SetJumpingNumbers();

                navMeshAgent.SetDestination(PlayerDirection);
                
                //play animation

                //do damage to hits in hitbox

                yield return new WaitForSeconds(0.4f);
                StartCoroutine(AttackEnemyCooldown());
                
            }

            yield return null;
        }
        
    }

    void DoAttack()
    {

    }
}
