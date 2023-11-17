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
    public float visionRange;
    public float wanderRadius;
    public float wanderTimer;
    public float groupSeekRange;
    public float seekCooldown;

    private Transform player;
    private Vector3 lastKnownPlayerPosition;
    private float timer;
    private NavMeshAgent navMeshAgent;
    private bool isCooldown;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;

        // Start with a random destination for wandering
        SetRandomDestination();
    }

    void Update()
    {
        // Check if player is in vision range
        if (player != null && IsPlayerInVisionRange())
        {

                lastKnownPlayerPosition = player.position;
                SeekPlayer();
                Debug.Log("Player Found"); 
        }
        else
        {
            if (!isCooldown)
            {
                WanderOrIdle();
            }
        }

        // Check for other AI to group up
        //GroupUpWithOtherAI();
    }

    bool IsPlayerInVisionRange()
    {
        return player != null && Vector3.Distance(transform.position, player.position) <= visionRange;
    }

    //This is not WORKING IDK WHY 
    //bool HasLineOfSightToPlayer()
    //{
    //    // Perform a raycast to check if there are obstacles between the AI and the player
    //    RaycastHit rayHit;
    //    if (player != null && Physics.Raycast(transform.position, player.position - transform.position, out rayHit, visionRange))
    //    {
    //        if (rayHit.collider.CompareTag("Player"))
    //        {
    //            Debug.Log("Sees player? " + HasLineOfSightToPlayer());
    //            return true; // Player is in line of sight
    //        }
    //    }
    //
    //        return false; // Player is obstructed
    //    
    //}


    void SeekPlayer()
    {
        Debug.Log("Seeking player");

        // Move towards the last known player position
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(lastKnownPlayerPosition);
        }

        // Start the cooldown timer
        StartCooldown();
    }

    void WanderOrIdle()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            // If timer is greater than wanderTimer, reset timer and set new destination
            SetRandomDestination();
            timer = 0;
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

    void StartCooldown()
    {
        // Start the cooldown timer
        isCooldown = true;
        Invoke("EndCooldown", seekCooldown);
    }

    void EndCooldown()
    {
        // End the cooldown timer
        isCooldown = false;
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
