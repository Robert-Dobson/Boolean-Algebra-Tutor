using UnityEngine;
using System.Data.SQLite;
using System;

public class DBManager : MonoBehaviour
{
    static private string connectionString; //Stores the location of the database
    static SQLiteConnection databaseConnection; //SQLite connection to databse
    static SQLiteCommand databaseCMD; //SQLite Commands

    private void Start()
    {
        //Set connection string to location of database and create new SQLite connection to it
        connectionString = "URI=file:" + Application.dataPath + "/BooleanAlgebraTutorDB.db; Version=3";
        databaseConnection = new SQLiteConnection(connectionString);
    }


    public int CheckCredentials(string username, string password)
    {
        //Create SQL command to fetch any account ID which has the entered username and password.
        SQLiteDataReader dbDataReader;  
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText = 
        @"
            SELECT AccountID 
            FROM Accounts 
            WHERE Username= $username and Password= $password 
        ";
        // Set username and password parameters (to protect against SQL injection) to username and password entered
        databaseCMD.Parameters.AddWithValue("$username", username);
        databaseCMD.Parameters.AddWithValue("$password", password);
        int AccountID = -1; //Default -1 for when no records are found.
       
        //Open Database connection and execute the SQL command then close the connection
        databaseConnection.Open();
        dbDataReader = databaseCMD.ExecuteReader();
        while (dbDataReader.Read()) //Every iteration is for each record found
        {
            AccountID = Convert.ToInt32(dbDataReader["AccountID"]);
        }
        dbDataReader.Close();
        databaseConnection.Close();
        return AccountID;
    }
}
