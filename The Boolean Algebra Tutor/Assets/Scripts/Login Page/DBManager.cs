using UnityEngine;
using System.Data.SQLite;
using System;
using System.Security.Cryptography;
using System.Text;

public class DBManager : MonoBehaviour
{
    static private string connectionString; //Stores the location of the database
    static SQLiteConnection databaseConnection; //SQLite connection to databse
    static SQLiteCommand databaseCMD; //SQLite Commands

    private void Start()
    {
        //Set connection string to location of database and create new SQLite connection to it
        connectionString = "URI=file:" + Application.streamingAssetsPath + "/BooleanAlgebraTutorDB.db; Version=3";
        databaseConnection = new SQLiteConnection(connectionString);
    }


    static public int CheckCredentials(string username, string password)
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

    static public Tuple<int, string, string, string, int> GetUserDetails(int accountID)
    {
        //Create SQL command to get all user details corresponding to the provided accountID.
        SQLiteDataReader dbDataReader;
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            SELECT Username, FirstName, LastName, TypeOfAccount
            FROM Accounts
            WHERE AccountID = $accountID
        ";
        
        //Add the provided account id as the accountID parameter (to protect against SQL injection)
        databaseCMD.Parameters.AddWithValue("$accountID", accountID);
        
        //Declare the username, firstname, lastname, accounttype variables and give default values
        string username = "";
        string firstName = "";
        string lastName = "";
        int accountType = -1;

        //Execute the SQL command on the database and assign the username, firstname, etc. to their corresponding variables.
        databaseConnection.Open();
        dbDataReader = databaseCMD.ExecuteReader();
        while (dbDataReader.Read())
        {
            username = Convert.ToString(dbDataReader["Username"]);
            firstName = Convert.ToString(dbDataReader["FirstName"]);
            lastName = Convert.ToString(dbDataReader["LastName"]);
            accountType = Convert.ToInt32(dbDataReader["TypeOfAccount"]);
        }
        dbDataReader.Close();
        databaseConnection.Close();

        Tuple<int, string, string, string, int> userDetails = Tuple.Create<int, string, string, string, int>(accountID,username,firstName,lastName,accountType);
        return userDetails;
    }

    static public string Sha256(string plainText)
    {
        //Create a hash
        using (SHA256 hasher = SHA256.Create())
        {
            //Compute the hash, store as bytes
            byte[] bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(plainText));

            // Convert byte aray to a string
            string hash = "";
            foreach (byte aByte in bytes)
            {
                hash += aByte.ToString("x2");
            }
            return hash;
        }

    }

    static public void CreateAccount(string username, string firstName, string lastName, int accountType, string password)
    {
        //Create SQL command which creates a new record in the Accounts table
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            INSERT INTO Accounts 
            (Username, Password, FirstName, LastName, TypeOfAccount)
            VALUES 
            ($username, $password, $firstName, $lastName, $typeOfAccount)
        ";

        //Assign the parameters to protect against SQL injection
        databaseCMD.Parameters.AddWithValue("$username", username);
        databaseCMD.Parameters.AddWithValue("$password", password);
        databaseCMD.Parameters.AddWithValue("$firstName", firstName);
        databaseCMD.Parameters.AddWithValue("$lastName", lastName);
        databaseCMD.Parameters.AddWithValue("$typeOfAccount", accountType);
        
        //Open database connection, execute command then close it
        databaseConnection.Open();
        databaseCMD.ExecuteNonQuery();
        databaseConnection.Close();

    }

    static public void UpdateAccount(int accountID, string username, string newFirstName, string newLastName, int newAccountType)
    {
        //Create SQL command which updates the record in the Accounts table for passed accountID
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            UPDATE Accounts
            SET Username = $username, FirstName= $firstName, LastName= $lastName, TypeOfAccount= $typeOfAccount
            WHERE AccountID = $accountID
        ";

        //Assign the parameters to protect against SQL injection
        databaseCMD.Parameters.AddWithValue("$username", username);
        databaseCMD.Parameters.AddWithValue("$firstName", newFirstName);
        databaseCMD.Parameters.AddWithValue("$lastName", newLastName);
        databaseCMD.Parameters.AddWithValue("$typeOfAccount", newAccountType);
        databaseCMD.Parameters.AddWithValue("$accountID", accountID);

        //Open database connection, execute command then close it
        databaseConnection.Open();
        databaseCMD.ExecuteNonQuery();
        databaseConnection.Close();
    }

    //Returns true if the username is already being used in a record in the Accounts Table
    static public bool UserExists(string username)
    {
        //Create SQL Command which returns number of records that have the username given as argument
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            SELECT count(*)
            FROM Accounts
            WHERE Username = $username
        ";

        //Assign SQL paramter for the username to prevent SQL injection
        databaseCMD.Parameters.AddWithValue("$username", username);

        //Execute command and hold the number of records return
        int numberOfRecords = 0;
        databaseConnection.Open();
        numberOfRecords = Convert.ToInt32(databaseCMD.ExecuteScalar());
        databaseConnection.Close();
        //If there's no records return false else return true.
        if (numberOfRecords == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
