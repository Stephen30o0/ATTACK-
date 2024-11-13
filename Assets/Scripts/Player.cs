using System;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public static Player Instance { get; private set; }
    public static event Action<int> OnHealthChanged; // Event declaration

    [SerializeField]
    private GameObject deathScreen; // Reference to the death screen UI
    private int health = 100;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player took " + damage + " damage! Remaining health: " + health);

        // Clamp health to prevent going below zero
        if (health < 0)
        {
            health = 0;
        }

        // Invoke the OnHealthChanged event
        OnHealthChanged?.Invoke(health);

        if (health <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        health = 100;
        OnHealthChanged?.Invoke(health); // Notify subscribers when health is reset
    }

    public int GetHealth() 
    {
        return health; // Optional: Getter for current health
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        
        // Show the death screen
        if (deathScreen != null)
        {
            deathScreen.SetActive(true); // Activate the death screen
            Time.timeScale = 0; // Pause the game (optional)
        }
        
        // Optionally disable player controls or other components here
    }
}