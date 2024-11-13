using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button takeDamageButton;
    public Button startGameButton;
    public Button spawnEnemiesButton;
    public Player player; // Reference to Player script
    public HealthUI healthUI; // Reference to HealthUI script
    private EnemyFactory enemyFactory; // Reference to EnemyFactory
    private bool isEnemyActive = false;

    private void Start()
    {
        // Find the EnemyFactory instance in the scene
        enemyFactory = FindObjectOfType<EnemyFactory>();

        takeDamageButton.onClick.AddListener(() => TakeDamage());
        startGameButton.onClick.AddListener(() => GameManager.Instance.StartGame());
        spawnEnemiesButton.onClick.AddListener(SpawnEnemies);

        // Initialize health display
        healthUI.UpdateHealthUI(player.GetHealth());

        // Subscribe to health change event
        Player.OnHealthChanged += healthUI.UpdateHealthUI;
    }

    private void TakeDamage()
    {
        player.TakeDamage(10);
    }

    private void SpawnEnemies()
    {
        if (!isEnemyActive) // Check if no enemy is currently active
        {
            isEnemyActive = true; // Set the flag to true
            Enemy alien = enemyFactory.CreateEnemy("Alien");
            Debug.Log("Spawned Alien");
            // Remove this line: alien.Attack(); 
            StartCoroutine(WaitForEnemyToDie(alien)); // Wait for the enemy to be destroyed
        }
        else
        {
            Debug.Log("An enemy is already active!"); // Optional: Log message if trying to spawn another enemy
        }
    }

    private IEnumerator WaitForEnemyToDie(Enemy enemy)
    {
        // Wait until the enemy is destroyed
        while (enemy != null)
        {
            yield return null; // Wait for the next frame
        }
        
        isEnemyActive = false; // Reset the flag when the enemy is destroyed
    }
}