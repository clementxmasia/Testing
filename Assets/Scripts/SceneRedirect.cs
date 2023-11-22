using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRedirect : MonoBehaviour
{
    // Public variables to set in the Unity Editor
    public float secondsToWait = 5f; // Time to wait before redirecting
    public int targetSceneIndex = 1; // Index of the scene to redirect to

    private void Start()
    {
        // Invoke the Redirect function after the specified seconds
        Invoke("RedirectToScene", secondsToWait);
    }

    private void RedirectToScene()
    {
        // Load the target scene by index
        SceneManager.LoadScene(targetSceneIndex);
    }
}