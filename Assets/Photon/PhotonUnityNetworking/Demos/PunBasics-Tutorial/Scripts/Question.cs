using UnityEngine;

public class Question : MonoBehaviour
{
    public GameObject targetObject; // Reference to the GameObject you want to activate.

    public void HandleLetter(string letter)
    {
        // Handle the received letter here, e.g., display it or perform some action
        Debug.Log("Received letter: Ping " + letter);

        // Activate the targetObject.
        targetObject.SetActive(true);
        
        // You can add your logic here to handle the letter in the context of your game or application.
    }
}
