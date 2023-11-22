using UnityEngine;
using UnityEngine.UI;

public class TextEditor : MonoBehaviour
{
    public Text textInputField;

    void Start()
    {
        // Retrieve the text from PlayerPrefs (stored from another scene)
        string receivedText = PlayerPrefs.GetString("TextToEdit", "");

        // Set the text in the input field
        textInputField.text = receivedText;
    }

    // Call this function when the player wants to save the edited text
    public void SaveText()
    {
        // Get the edited text from the input field
        string editedText = textInputField.text;

        // Save the edited text to PlayerPrefs
        PlayerPrefs.SetString("TextToEdit", editedText);
        PlayerPrefs.Save();

        // Optionally, you can load another scene here
        // SceneManager.LoadScene("YourNextSceneName");
    }
}
