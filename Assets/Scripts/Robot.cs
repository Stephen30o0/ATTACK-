using System.Collections;
using UnityEngine;

public class Robot : Enemy 
{
    private bool hasAttacked = false; // Flag to check if the robot has already attacked

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the Player
        if (other.CompareTag("Player") && !hasAttacked) // Ensure it only attacks once
        {
            Debug.Log("Robot collided with player!");
            Player.Instance.TakeDamage(10); // Deal damage to player
            hasAttacked = true; // Set the flag to true
            
            // Start coroutine to destroy this enemy after a delay
            StartCoroutine(DestroyAfterDelay(1f)); // Delay of 1 second before destruction
        }
    }

    public override void Attack()
    {
        // The attack method can remain as is, but now it's not necessary for damage dealing
        if (!hasAttacked) // Check if it hasn't attacked yet
        {
            Debug.Log("Robot attacks");
            Player.Instance.TakeDamage(10); // Deal damage to player
            hasAttacked = true; // Set the flag to true

            // Start coroutine to destroy this enemy after a delay
            StartCoroutine(DestroyAfterDelay(1f)); // Delay of 1 second before destruction
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); // Remove this enemy from the scene
    }
}