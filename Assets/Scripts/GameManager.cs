using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private GameObject deathScreen; // Reference to your death screen UI

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

    public void StartGame()
    {
        Debug.Log("Game Started");
        Player.Instance.ResetHealth();
        
        // Hide death screen when starting a new game
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
            Time.timeScale = 1; // Ensure game time is running
        }
    }

    public void ShowDeathScreen()
    {
        if (deathScreen != null)
        {
            deathScreen.SetActive(true); // Show death screen
            Time.timeScale = 0; // Pause game time
        }
    }

    public void Respawn()
    {
        Player.Instance.ResetHealth(); // Reset player's health
        
        // Hide the death screen after respawning
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
            Time.timeScale = 1; // Resume game time after respawn
        }
        
        Debug.Log("Player respawned");
        
        // Optionally, you can reload the current scene or reset player position here
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing in editor
#else
        Application.Quit(); // Quit application (works in built application)
#endif
    }
}