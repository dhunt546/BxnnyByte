using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;

public class EnemyAI : MonoBehaviour

{
    //Regan Ly
    //Need to add: weighted desions, fix colliders bc theyre too chunky. Or make new alien type that is smaller as the basic alien
    public float defaultvisionRange;
    public float huntingvisionRange;
    private float currentVision;
    public float wanderRadius;
    public float wanderTime;
    private float wanderTimer;
    public float groupSeekRange;


    public float seekCooldown;
    private float cooldownTimer = 0;
    private bool isCooldown;

    private Transform player;
    private Vector3 lastKnownPlayerPosition;
    private NavMeshAgent navMeshAgent;
    
    


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, currentVision);
        
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        wanderTimer = wanderTime;
        currentVision = defaultvisionRange;
        // Start with a random destination for wandering
        SetRandomDestination();
    }

    void Update()
    {
        lastKnownPlayerPosition = player.position;

        // Check if player is in vision range and seek. Start seeking cooldown
        if (player != null && IsPlayerInVisionRange())
        {
            SeekPlayer();
            
        }
        else
        {
           WanderOrIdle(); 
        }

        // Check for other AI to group up
        //GroupUpWithOtherAI();
    }

    bool IsPlayerInVisionRange()
    {
        return player != null && Vector3.Distance(transform.position, player.position) <= currentVision;
    }
    void SeekPlayer()
    {
        
        // Move towards the last known player position
        if (navMeshAgent != null)
        {
            
            navMeshAgent.SetDestination(lastKnownPlayerPosition);
            //set vision higher to chase player
            currentVision = huntingvisionRange;
         
        }

        if (!IsPlayerInVisionRange())
        {
            Debug.Log("started cooldown");
            StartCooldown();
        }
    }
    void StartCooldown()
    {
        //stop the cooldown timer first to restart it.
        if (isCooldown)
        {
            StopCoroutine(CooldownTimer());
        }
        // Start the cooldown timer
        isCooldown = true;
        cooldownTimer = seekCooldown;
        StartCoroutine(CooldownTimer());
    }

    IEnumerator CooldownTimer()
    {
        while (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            yield return null;
            if (cooldownTimer < 0)
            {
                cooldownTimer = 0;
            }
        }

        // End the cooldown timer
        isCooldown = false;
        currentVision = defaultvisionRange;
    }


    void WanderOrIdle()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderTime)
        {
            // If timer is greater than wanderTimer, reset timer and set new destination
            SetRandomDestination();          
            wanderTimer = 0;
        }
    }

    void SetRandomDestination()
    {
        if (navMeshAgent != null)
        {
            // Generate a random point within the wander radius
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            RaycastHit rayHit;

            // Perform a raycast to check if the random point is obstructed by obstacles
            if (!Physics.Raycast(transform.position, randomDirection - transform.position, out rayHit, wanderRadius))
            {
                // Perform a NavMesh sample to check if the random point is on the NavMesh
                if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1))
                {
                    // Set the destination to the random point
                    navMeshAgent.SetDestination(hit.position);
                }
            }
        }
    }




    // void GroupUpWithOtherAI()
    // {
    //     //Debug.Log("Grouping up with otherAi");
    //     // Find all other Enemy AI in the scene
    //     GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //
    //     if (!IsPlayerInVisionRange())
    //     {
    //         foreach (GameObject enemy in enemies)
    //         {
    //             // Check if the other AI is within the group seek range and not the same AI
    //             if (enemy != gameObject && Vector3.Distance(transform.position, enemy.transform.position) <= groupSeekRange)
    //             {
    //                 // Move towards the other AI
    //                 navMeshAgent.SetDestination(enemy.transform.position);
    //             }
    //         }
    //     }
    // }s
}
