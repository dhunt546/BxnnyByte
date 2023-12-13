using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //by Devin Hunt and Regan Ly
    public float moveSpeed = 5.0f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    public GameObject PlayerDirection;
    public string playerDirection = "Forward";

    public bool isMoving;
   
    public int PlayerHealth = 5;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
    
        if (movement != Vector2.zero)
        {
            isMoving = true;

            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg; 
            
            PlayerDirection.transform.rotation = Quaternion.Euler(0, 0, angle);

            if (angle >= -45 && angle < 45)
                playerDirection = "Right";
            else if (angle >= 45 && angle < 135)
                playerDirection = "Backward";
            else if (angle >= 135 || angle < -135)
                playerDirection = "Left";
            else
                playerDirection = "Forward";         
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