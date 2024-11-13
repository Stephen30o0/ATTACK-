using UnityEngine;

public abstract class Enemy : MonoBehaviour // Inherit from MonoBehaviour
{
    public int health = 100; // Health of the enemy
    public float attackRange = 1.5f; // Range within which the enemy can attack
    public float moveSpeed = 3.0f; // Speed at which the enemy moves
    protected Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        // Find the player in the scene
        playerTransform = GameObject.FindWithTag("Player").transform; // Ensure your Player GameObject has the tag "Player"
    }

    private void Update()
    {
        MoveTowardsPlayer();
        CheckAttack();
    }

    protected void MoveTowardsPlayer()
    {
        if (playerTransform != null)
        {
            // Move towards the player
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    protected void CheckAttack()
    {
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            Attack(); // Call the attack method when in range
        }
    }

    public abstract void Attack(); 

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{this.GetType().Name} took {damage} damage! Remaining health: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{this.GetType().Name} has died!");
        Destroy(gameObject); // Remove the enemy from the scene
    }
}