using UnityEngine;
using UnityEngine.UI;

public class UIMouseInteraction : MonoBehaviour
{
    private string storedLetter = ""; // Initialize as an empty string
    public GameObject uiElement;

    // Update is called once per frame
    void Update()
    {
        if (!string.IsNullOrEmpty(storedLetter))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Trigger the event when a letter is stored and player presses a key
                Debug.Log("Event triggered with storedLetter: " + storedLetter);

                // Reset storedLetter to empty string after the event
                storedLetter = "";
            }
        }
    }

    // OnTriggerEnter is called when a collision is detected
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Letter"))
        {
            // Store the name of the "Letter"
            storedLetter = other.name;
            Debug.Log("Stored letter: " + storedLetter);
        }
        if (other.CompareTag("Question") && !string.IsNullOrEmpty(storedLetter))
        {
            // The player collided with a "Question" tag and storedLetter has a value
            Debug.Log("Player collided with a question!");
            uiElement.SetActive(true);

            // You can trigger an event here, e.g., showing UI or performing an action.
        }
    }
}