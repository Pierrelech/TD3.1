using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class EndGameDisplay : MonoBehaviour
{
    // References to the UI elements
    public GameObject endGamePanel;  // The panel that holds the results (set in Inspector)
    public TextMeshProUGUI timeText;            // Text to display the time
    public TextMeshProUGUI collisionText;       // Text to display the number of collisions
    public TextMeshProUGUI messageText;         // Text to display the end message

    public GameObject player;

    private float startTime;         // Time when the mission starts
    private int collisionCount = -2;  // Number of collisions

    private bool timerStarted = false;  // To ensure timer starts only when the player does an action

    void Start()
    {
        // Initially hide the end game panel
        endGamePanel.SetActive(false);
    }

    // Call this method to start the timer when the player performs an action (e.g., presses any key)
    public void StartTimer()
    {
        if (!timerStarted)
        {
            startTime = Time.time;
            timerStarted = true;
        }
    }

    // Call this method to count collisions
    public void OnCollisionDetected()
    {
        collisionCount++;
    }

    public void DisablePlayerMovement()
    {
        if (player != null)
        {
            if (player.GetComponent<CharacterController>())
                player.GetComponent<CharacterController>().enabled = false;

            MonoBehaviour movementScript = player.GetComponent<MonoBehaviour>();
            if (movementScript != null)
                movementScript.enabled = false;
        }
    }

    // Display the results at the end of the mission
    public void ShowEndGameResults()
    {
        // Calculate the time taken
        float elapsedTime = Time.time - startTime;

        // Update the text components with the results
        timeText.text = "Time: " + elapsedTime.ToString("F2") + " seconds";
        collisionText.text = "Collisions: " + collisionCount;

        // Determine the end message based on the number of collisions
        if (collisionCount == 0)
        {
            messageText.text = "Mission Accomplished!";
        }
        else
        {
            messageText.text = "Try Again...";
        }

        // Show the end game panel
        endGamePanel.SetActive(true);

        DisablePlayerMovement();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;                
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
