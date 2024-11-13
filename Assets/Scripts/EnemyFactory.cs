using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject alienPrefab; // Assign in Inspector
    public GameObject robotPrefab; // Assign in Inspector
    public Transform[] spawnPoints; // Array of spawn points

    public Enemy CreateEnemy(string type)
    {
        Transform spawnPoint = GetRandomSpawnPoint(); // Get a random spawn point

        switch (type)
        {
            case "Alien":
                return Instantiate(alienPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Alien>();
            case "Robot":
                return Instantiate(robotPrefab, spawnPoint.position, Quaternion.identity).GetComponent<Robot>();
            default:
                throw new System.ArgumentException("Invalid enemy type");
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points set!");
            return null;
        }
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }
}