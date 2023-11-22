using System;
using System.Data;
using MySql.Data.MySqlClient;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private MySqlConnection connection;
    private string server = "localhost";
    private string database = "unity";
    private string uid = "root";
    private string password = "";

    // Start is called before the first frame update
    void Start()
    {
        ConnectToDatabase();
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

    // Function to close the database connection
    private void CloseConnection()
    {
        if (connection != null && connection.State == ConnectionState.Open)
        {
            connection.Close();
            Debug.Log("Connection closed.");
        }
    }

    // This method is called when the script is destroyed
    private void OnDestroy()
    {
        CloseConnection();
    }
}
