using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour

{
    //Regan Ly
    //Need to add: weighted desions, fix colliders bc theyre too chunky. Or make new alien type that is smaller as the basic alien
    public float visionRange = 10f;
    public float wanderRadius = 5f;
    public float wanderTimer = 3f;
    public float groupSeekRange = 15f;

    private Transform player;
    private Vector3 lastKnownPlayerPosition;
    private float timer;
    private NavMeshAgent navMeshAgent;

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
        if (IsPlayerInVisionRange())
        {
            lastKnownPlayerPosition = player.position;
            SeekPlayer();
        }
        else
        {
            // If player is not in vision range, wander or idle
            WanderOrIdle();
        }

        // Check for other AI to group up
        //GroupUpWithOtherAI();
    }

    bool IsPlayerInVisionRange()
    {
        return Vector3.Distance(transform.position, player.position) <= visionRange;
    }

    void SeekPlayer()
    {
       // Debug.Log("seeking player");
        // Move towards the last known player position
        navMeshAgent.SetDestination(lastKnownPlayerPosition);
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
        // Generate a random point within the wander radius
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        Vector3 finalPosition = hit.position;

        // Set the destination to the random point
        navMeshAgent.SetDestination(finalPosition);
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
