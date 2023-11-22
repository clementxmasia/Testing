using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    // Public variable to set in the Unity Editor
    public float countdownSeconds = 10f; // Initial countdown time
    public int targetSceneIndex = 1; // Index of the scene to redirect to

    public Text countdownText;
    private float currentTime;

    private void Start()
    {
        // Get the Text component attached to the GameObject
        //countdownText = GetComponent<Text>();

        // Set the initial time
        currentTime = countdownSeconds;

        // Invoke the UpdateTimer function every second
        InvokeRepeating("UpdateTimer", 0f, 1f);
    }

    private void UpdateTimer()
    {
        // Update the timer
        currentTime -= 1f;

        // Update the text display
        countdownText.text = Mathf.Ceil(currentTime).ToString();

        // Check if the timer has reached zero
        if (currentTime <= 0f)
        {
            // Redirect to the target scene
            SceneManager.LoadScene(targetSceneIndex);

            // Stop the timer (optional, depending on your game logic)
            CancelInvoke("UpdateTimer");
        }
    }
}
