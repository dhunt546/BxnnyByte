using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour  //Edited by Regan Ly   Source: ChatGPT

{
        public Slider healthSlider;
        public float maxHealth = 100f;
        public float currentHealth;

        void Start()
        {
            currentHealth = maxHealth;
            UpdateHealthBar();
        }

        void Update()
        {

        }

        // Function to reduce health
        public void TakeDamage(float damageAmount)
        {
            currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure health doesn't go below 0 or above maxHealth
            UpdateHealthBar();

            // Check if the player is dead (health is 0)
            if (currentHealth == 0f)
            {
                Die();
            }
        }

        // Function to update the health bar UI
        void UpdateHealthBar()
        {
            float healthPercentage = currentHealth / maxHealth;
            healthSlider.value = healthPercentage;
        }

        // Function to handle player death
        void Die()
        {
            // Implement any death logic here (e.g., game over screen, respawn, etc.)
           // Debug.Log("Player has died!");
        }
}

