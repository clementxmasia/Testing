using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Server : MonoBehaviour
{
    [SerializeField] GameObject welcomePanel;
    [SerializeField] Text user;
    [Space]
    [SerializeField] InputField username;
    [SerializeField] InputField password;

    [SerializeField] Text errorMessages;
    [SerializeField] GameObject progressCircle;

    [SerializeField] Button loginButton;

    [SerializeField] string url;

    WWWForm form;

    public void OnLoginButtonClicked()
    {
        loginButton.interactable = false;
        progressCircle.SetActive(true);
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        form = new WWWForm();

        form.AddField("username", username.text);
        form.AddField("password", password.text);

        WWW w = new WWW(url, form);
        yield return w;

        if (w.error != null)
        {
            errorMessages.text = "404 not found!";
            Debug.Log("<color=red>" + w.text + "</color>"); //error
        }
        else
        {
            if (w.isDone)
            {
                if (w.text.Contains("error"))
                {
                    errorMessages.text = "invalid username or password!";
                    Debug.Log("<color=red>" + w.text + "</color>"); //error
                }
                else
                {
                    // Open welcome panel
                    welcomePanel.SetActive(true);
                    user.text = username.text;
                    Debug.Log("<color=green>" + w.text + "</color>"); //user exist

					PlayerPrefs.SetString("TextToEdit", username.text);
        			PlayerPrefs.Save();

                    // Write username to a text file and override existing content
                    string filePath = WriteUsernameToFile(username.text);

                    // Print the location of the file to the console
                    Debug.Log("File saved at: " + filePath);
                }
            }
        }

        loginButton.interactable = true;
        progressCircle.SetActive(false);

        w.Dispose();
    }

    private string WriteUsernameToFile(string username)
    {
        // Specify the path to the text file (change it as needed)
        string filePath = Application.persistentDataPath + "/usernames.txt";

        // Write the username to the text file, overwriting existing content
        File.WriteAllText(filePath, username);

        Debug.Log("Username written to file: " + username);

        return filePath;
    }
}
