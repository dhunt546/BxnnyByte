using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    public GameObject PlayerDirection;
    public string playerDirection = "Forward";

    public bool isMoving;
    bool isAttacking;


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // animator.SetFloat("Horizontal", movement.x);
        // animator.SetFloat("vertical", movement.y);
        // animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement != Vector2.zero)
        {
            isMoving = true;
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            // angle will be in the range of -180 to 180 degrees
            // You can use angle to determine the direction or rotation of the player
            // For example, you can rotate the player object to face the direction:
            PlayerDirection.transform.rotation = Quaternion.Euler(0, 0, angle);
            if (angle >= -45 && angle < 45)
                playerDirection = "Left";
            else if (angle >= 45 && angle < 135)
                playerDirection = "Backward";
            else if (angle >= 135 || angle < -135)
                playerDirection = "Right";
            else
                playerDirection = "Forward";
            //Debug.Log("player is facing "+playerDirection);
        }
        else
        {
            isMoving = false;
        }

    }

   
   


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);


    }

}