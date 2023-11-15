using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private AIData aiData;
    public float moveSpeed = 5f; // Movement speed of the enemy

    private void Start()
    {
        // Get the AIData script attached to the same GameObject
        aiData = GetComponent<AIData>();
    }
    void Update()
    {
        if (aiData != null && aiData.currentTarget != null)
        {
            // Calculate the direction to the target
            Vector3 directionToTarget = aiData.currentTarget.position - transform.position;

            // Calculate the distance to the target
            float distanceToTarget = directionToTarget.magnitude;

            // Determine the obstacle weight (you may have your own logic for this)
            float obstacleWeight = CalculateObstacleWeight();

            // Determine the target weight (you may have your own logic for this)
            float targetWeight = CalculateTargetWeight(distanceToTarget);

            // Calculate the final movement vector based on weights
            Vector3 movement = CalculateMovement(obstacleWeight, targetWeight, directionToTarget);

            // Move the enemy
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }

    float CalculateObstacleWeight()
    {
        // Your logic for calculating obstacle weight goes here
        // Return a value between 0 and 1 based on the obstacle detection logic
        return 0.5f;
    }

    float CalculateTargetWeight(float distanceToTarget)
    {
        // Your logic for calculating target weight goes here
        // Return a value between 0 and 1 based on the distance to the target
        return Mathf.Clamp01(1 - distanceToTarget / 10f);
    }

    Vector3 CalculateMovement(float obstacleWeight, float targetWeight, Vector3 directionToTarget)
    {
        // Combine obstacle and target weights to determine the final movement vector
        // You can adjust the weights based on your desired behavior
        float combinedWeight = obstacleWeight + targetWeight;

        if (combinedWeight > 0)
        {
            // Calculate the weighted average of obstacle and target directions
            Vector3 obstacleAvoidance = CalculateObstacleAvoidance();
            Vector3 weightedDirection = (obstacleWeight * obstacleAvoidance + targetWeight * directionToTarget) / combinedWeight;

            // Return the normalized movement vector
            return weightedDirection.normalized;
        }
        else
        {
            // No weights, return zero movement
            return Vector3.zero;
        }
    }

    Vector3 CalculateObstacleAvoidance()
    {
        // Your logic for obstacle avoidance goes here
        // Return a vector representing the direction to avoid obstacles
        return Vector3.zero;
    }
}


//    public GameObject player;
//    public float speed;
//    public float distancebetween;
//
//    private float distance;
//    void Start()
//    {
//        
//    }
//
//    
//    void Update()
//    {
//        distance = Vector2.Distance(transform.position, player.transform.position);
//        Vector2 direction = player.transform.position - transform.position;
//        direction.Normalize();
//        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//
//        if (distance <= distancebetween) 
//        {
//            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
//            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
//        
//        }
//
//
//    }
//}
