using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    private void OnEnable()
    {
        Player.OnHealthChanged += UpdateHealthUI; // Subscribe to the event
    }

    private void OnDisable()
    {
        Player.OnHealthChanged -= UpdateHealthUI; // Unsubscribe from the event
    }

    public void UpdateHealthUI(int currentHealth)
    {
        healthText.text = "Health: " + currentHealth; // Update the health text
    }
}