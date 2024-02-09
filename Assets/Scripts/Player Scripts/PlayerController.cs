using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    //by Devin Hunt and Regan Ly
    private float moveSpeed = 5.0f;
    private Rigidbody2D rb;
    public PlayerStats playerStats;
    private PlayerAnimation animator;
    [SerializeField]private float slowedSpeed;
    [SerializeField]private float normalSpeed;
    Vector2 movement;
    public GameObject PlayerDirection;
    public string playerDirection = "Forward";

    public bool isPlayerDead = false;
    public Slider healthSlider;
    public float maxHealth = 100f;

    //public int PlayerHealth = 5;
    private void Start()
    {
        moveSpeed = normalSpeed;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayerAnimation>();

        playerStats.playerHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            TakeDamage(5f);
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

    //if the player is attacking, slow their speed greatly

    public IEnumerator SlowSpeed()
    {
        moveSpeed = slowedSpeed;
        yield return new WaitForSeconds(animator._attackAnimTime);
        moveSpeed = normalSpeed;
    }

    public void PlayerDead()
    {
        isPlayerDead = true;
    }
    // Function to reduce health
    public void TakeDamage(float damageAmount)
    {
        Debug.Log("Taking damage");
        StartCoroutine(animator.OnHit());
        playerStats.playerHealth = Mathf.Clamp(playerStats.playerHealth, 0f, maxHealth); // Ensure health doesn't go below 0 or above maxHealth
        UpdateHealthBar();
        playerStats.playerHealth -= damageAmount;
        //PROBLEM: 

        // Check if the player is dead (health is 0)
        if (playerStats.playerHealth == 0f)
        {
            PlayerDead();
        }
    }

    void UpdateHealthBar()
    {
        float healthPercentage = playerStats.playerHealth / maxHealth;
        healthSlider.value = healthPercentage;
    }
}
