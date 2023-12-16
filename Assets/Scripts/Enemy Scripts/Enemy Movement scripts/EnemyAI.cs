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
    public float wanderTimer;
    public float groupSeekRange;


    public float seekCooldown;
    private float cooldownTimer = 0;
    private bool isCooldown;

    private Transform player;
    private Vector3 lastKnownPlayerPosition;
    private NavMeshAgent navMeshAgent;
    private float previousDistanceToPlayer;
    private bool wasPlayerInVisionRange = false;


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
        wasPlayerInVisionRange = false;

    }

    void Update()
    {
        //Debug.Log(cooldownTimer);
        lastKnownPlayerPosition = player.position;

        // Check if player is in vision range and seek. Start seeking cooldown
        if ((player != null && IsPlayerInVisionRange()) || isCooldown == true)
        {
            SeekPlayer();
            
        }
        else
        {
           WanderOrIdle(); 
        }

        if (!IsPlayerInVisionRange() && wasPlayerInVisionRange)
        {
            // Player has just left the vision range, perform any necessary actions
            Debug.Log("Player just left vision range");
            StartCooldown();
            // For example, start a cooldown or trigger some behavior
        }

        // Check for other AI to group up
        // GroupUpWithOtherAI();

        // Update the previous distance for the next frame
        UpdatePreviousDistance();
    
    // Check for other AI to group up
    //GroupUpWithOtherAI();
}



    bool IsPlayerInVisionRange()
    {
        return player != null && Vector3.Distance(transform.position, player.position) <= currentVision;
    }

    void UpdatePreviousDistance()
    {
        // Update the previous distance between AI and player for the next frame
        previousDistanceToPlayer = player != null ? Vector3.Distance(transform.position, player.position) : float.MaxValue;

        // Update the flag indicating whether the player was previously in vision range
        wasPlayerInVisionRange = IsPlayerInVisionRange();
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
        currentVision = defaultvisionRange;
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
