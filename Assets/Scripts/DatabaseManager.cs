using System;
using System.Data;
using MySql.Data.MySqlClient;
using TMPro;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private MySqlConnection connection;
    private string server = "localhost";
    private string database = "unity";
    private string uid = "root";
    private string password = "";

    private string question;
    private string answer;
    public string selectedLetter; // Public variable to store the provided letter

    public TextMeshProUGUI questionTextMeshPro;
    public TextMeshProUGUI answerTextMeshPro;

    // Additional TextMeshPro components for random words
    public TextMeshProUGUI wordText1;
    public TextMeshProUGUI wordText2;
    public TextMeshProUGUI wordText3;

    // Start is called before the first frame update
    void Start()
    {
        ConnectToDatabase();
        RetrieveQuestionAndAnswer();
        RetrieveRandomWords();
    }

    // Function to connect to the database
    private void ConnectToDatabase()
    {
        string connectionString = $"Server={server};Database={database};User ID={uid};Password={password};Pooling=true;";

        try
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
            Debug.Log("Connected to the database!");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error connecting to the database: {ex.Message}");
        }
    }

    // Function to retrieve a random row from the unityquestion table
    private void RetrieveQuestionAndAnswer()
    {
        try
        {
            // Create a MySqlCommand object and specify the SQL query to get a random row
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM unityquestion ORDER BY RAND() LIMIT 1", connection);

            // Execute the query and get the result
            using (IDataReader reader = cmd.ExecuteReader())
            {
                // Check if there is a row
                if (reader.Read())
                {
                    // Get the value of the "Letter" column
                    string letter = reader["Letter"].ToString();

                    // Check if the "Letter" column matches the selected letter
                    if (letter.Equals(selectedLetter, StringComparison.OrdinalIgnoreCase))
                    {
                        // Assign the values to the public variables
                        question = reader["Question"].ToString();
                        answer = reader["Answer"].ToString();

                        // Assign the values to the TextMeshPro components
                        questionTextMeshPro.text = question;
                        answerTextMeshPro.text = answer;
                    }
                }
                else
                {
                    Debug.LogWarning("No rows found in the unityquestion table.");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error retrieving question and answer: {ex.Message}");
        }
    }

    // Function to retrieve random words from the random_words table
    private void RetrieveRandomWords()
    {
        Debug.Log("1");

        try
        {
            // Create a MySqlCommand object and specify the SQL query to get a random word
            MySqlCommand cmd = new MySqlCommand("SELECT word FROM random_words ORDER BY RAND() LIMIT 3", connection);
            Debug.Log("2");

            // Execute the query and get the result
            using (IDataReader reader = cmd.ExecuteReader())
            {
                // Check if there are rows
                Debug.Log("3");
                int count = 0;
                while (reader.Read() && count < 3)
                {
                    // Get the value of the "words" column
                    string word = reader["word"].ToString();

                    // Assign the values to the TextMeshPro components for random words
                    switch (count)
                    {
                        case 0:
                        Debug.Log("4");
                            wordText1.text = word;
                            break;
                        case 1:
                            wordText2.text = word;
                            break;
                        case 2:
                            wordText3.text = word;
                            break;
                    }

                    count++;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error retrieving random words: {ex.Message}");
        }
    }

    public void Next()
    {
        RetrieveRandomWords();
        RetrieveQuestionAndAnswer();
    }

    // Function to close the database connection
    private void CloseConnection()
    {
        if (connection != null && connection.State == ConnectionState.Open)
        {
            connection.Close();
            Debug.Log("Connection closed.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        questionTextMeshPro.text = question;
        answerTextMeshPro.text = answer;
        //RetrieveRandomWords();
    }

    // This method is called when the script is destroyed
    private void OnDestroy()
    {
        CloseConnection();
    }
}
