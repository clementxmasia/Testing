using UnityEngine;

public class Question : MonoBehaviour
{
    // This method is called when the "HandleLetter" message is sent to the script
    public void HandleLetter(string letter)
    {
        // Handle the received letter here, e.g., display it or perform some action
        Debug.Log("Received letter: Ping " + letter);

        // You can add your logic here to handle the letter in the context of your game or application.
    }
}
