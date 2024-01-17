using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //by Devin Hunt and Regan Ly
    public float moveSpeed = 5.0f;
    private Rigidbody2D rb;

    private PlayerAnimation animator;

    Vector2 movement;
    public GameObject PlayerDirection;
    public string playerDirection = "Forward";

    //public int PlayerHealth = 5;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement.Normalize();
    
        if (movement != Vector2.zero)
        {
            animator.isPlayerRunning = true;

            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg; 
            PlayerDirection.transform.rotation = Quaternion.Euler(0, 0, angle);

            //Set Direction of player if they have moved.
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
            animator.isPlayerRunning = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

}
