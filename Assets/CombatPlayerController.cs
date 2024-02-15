using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayerController : MonoBehaviour
{
    public float moveSpeed = 8f; // Speed of player movement
    public float tileSize = 2f; // Size of each tile

    private bool isMoving = false; // Flag to check if the player is currently moving
    private Vector3 targetPosition; // Target position for movement
    public LayerMask tileLayer;

    void Update()
    {
        // Check for player input to initiate movement
        if (!isMoving && Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(Vector3.left);
        }
        else if (!isMoving && Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(Vector3.right);
        }
    }
    TileScript GetTileAtPosition(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        if (hit.collider != null)
        {
            return hit.collider.GetComponent<TileScript>();
        }
        return null;
    }
    void MovePlayer(Vector3 direction)
    {
        // Calculate the target position for movement
        Vector3 nextPosition = transform.position + direction * tileSize;

        // Check if the next position is a dodging tile
        TileScript nextTile = GetTileAtPosition(nextPosition);
        if (nextTile != null && nextTile.isDodgingTile)
        {
            // Start coroutine for smooth movement
            targetPosition = nextPosition;
            StartCoroutine(MoveCoroutine());
        }


        IEnumerator MoveCoroutine()
        {
            isMoving = true;

            while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
            {
                // Move the player towards the target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Ensure the player reaches the exact target position
            transform.position = targetPosition;

            isMoving = false;
        }
    }
}
