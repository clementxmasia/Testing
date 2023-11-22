using TMPro;
using UnityEngine;

public class TextEditor : MonoBehaviour
{
    // Reference to the TextMeshPro component
    public TextMeshProUGUI textMeshPro;

    // Constant text to be used
    private const string constantText = "Hello, Unity!";

    void Start()
    {
        // Check if the TextMeshPro component is assigned
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component not assigned. Please assign it in the inspector.");
            return;
        }

        // Edit the text with the constant value
        EditText();
    }

    void EditText()
    {
        // Set the text of the TextMeshPro component to the constant value
        textMeshPro.text = constantText;
    }
}
