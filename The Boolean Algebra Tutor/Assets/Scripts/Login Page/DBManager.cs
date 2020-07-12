using UnityEngine;
using System.Data.SQLite;
using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.Video;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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

    static public Tuple<int, string, string, string> GetUserDetails(int accountID)
    {
        //Create SQL command to get all user details corresponding to the provided accountID.
        SQLiteDataReader dbDataReader;
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            SELECT Username, FirstName, LastName
            FROM Accounts
            WHERE AccountID = $accountID
        ";
        
        //Add the provided account id as the accountID parameter (to protect against SQL injection)
        databaseCMD.Parameters.AddWithValue("$accountID", accountID);
        
        //Declare the username, firstname and last name variables and give default values
        string username = "";
        string firstName = "";
        string lastName = "";

        //Execute the SQL command on the database and assign the username, firstname, etc. to their corresponding variables.
        databaseConnection.Open();
        dbDataReader = databaseCMD.ExecuteReader();
        while (dbDataReader.Read())
        {
            username = Convert.ToString(dbDataReader["Username"]);
            firstName = Convert.ToString(dbDataReader["FirstName"]);
            lastName = Convert.ToString(dbDataReader["LastName"]);
        }
        dbDataReader.Close();
        databaseConnection.Close();

        Tuple<int, string, string, string> userDetails = Tuple.Create<int, string, string, string>(accountID,username,firstName,lastName);
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

    static public bool CheckPasswordStrength(string password)
    {
        //Regex pattern that checks is password is 8 characters long, contain both upper and lower case letters, numbers and special characters. 
        string validationPattern = @"((?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!-/]|.*[:-@]|.*[\[-`]|.*[\{-~]).{8,})";
        
        //If password matches this regex pattern then return true (password is secure) else return false
        if(Regex.Match(password, validationPattern).Success)
        {
            return true;
        }
        else
        {
            return false;
        }
        

    }

    static public void CreateAccount(string username, string firstName, string lastName, string password)
    {
        //Create SQL command which creates a new record in the Accounts table
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            INSERT INTO Accounts 
            (Username, Password, FirstName, LastName)
            VALUES 
            ($username, $password, $firstName, $lastName)
        ";

        //Assign the parameters to protect against SQL injection
        databaseCMD.Parameters.AddWithValue("$username", username);
        databaseCMD.Parameters.AddWithValue("$password", password);
        databaseCMD.Parameters.AddWithValue("$firstName", firstName);
        databaseCMD.Parameters.AddWithValue("$lastName", lastName);
        
        //Open database connection, execute command then close it
        databaseConnection.Open();
        databaseCMD.ExecuteNonQuery();
        databaseConnection.Close();

    }

    static public void UpdateAccount(int accountID, string username, string newFirstName, string newLastName)
    {
        //Create SQL command which updates the record in the Accounts table for passed accountID
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            UPDATE Accounts
            SET Username = $username, FirstName= $firstName, LastName= $lastName
            WHERE AccountID = $accountID
        ";

        //Assign the parameters to protect against SQL injection
        databaseCMD.Parameters.AddWithValue("$username", username);
        databaseCMD.Parameters.AddWithValue("$firstName", newFirstName);
        databaseCMD.Parameters.AddWithValue("$lastName", newLastName);
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

    public static void ResetPassword(int accountID, string password)
    {
        //Create SQL command which updates the password in the record in the Accounts table for passed accountID
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            UPDATE Accounts
            SET Password = $password
            WHERE AccountID = $accountID
        ";

        //Assign the parameters to protect against SQL injection
        databaseCMD.Parameters.AddWithValue("$password", password);
        databaseCMD.Parameters.AddWithValue("$accountID", accountID);

        //Open database connection, execute command then close it
        databaseConnection.Open();
        databaseCMD.ExecuteNonQuery();
        databaseConnection.Close();

    }

    public static void DeleteAccount(int accountID)
    {
        //Create an SQL statement which will delete the given account id's record from accounts table
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            DELETE FROM Accounts
            WHERE AccountID = $accountID
        ";

        //Assign the parameters to protect against SQL injection
        databaseCMD.Parameters.AddWithValue("$accountID", accountID);

        //Open database connection, execute command then close it
        databaseConnection.Open();
        databaseCMD.ExecuteNonQuery();
        databaseConnection.Close();
    }

    public static void CreateQuestion(string name, string description, int difficulty, string question, string answer, int creator)
    {
        //Create SQL command which creates a new record in the Questions table
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            INSERT INTO Questions 
            (Name, Description, Difficulty, Question, Answer, Creator)
            VALUES 
            ($name, $description, $difficulty, $question, $answer, $creator)
        ";

        //Assign the parameters to protect against SQL injection
        databaseCMD.Parameters.AddWithValue("$name", name);
        databaseCMD.Parameters.AddWithValue("$description", description);
        databaseCMD.Parameters.AddWithValue("$difficulty", difficulty);
        databaseCMD.Parameters.AddWithValue("$question", question);
        databaseCMD.Parameters.AddWithValue("$answer", answer);
        databaseCMD.Parameters.AddWithValue("$creator", creator);

        //Open database connection, execute command then close it
        databaseConnection.Open();
        databaseCMD.ExecuteNonQuery();
        databaseConnection.Close();
    }

    public static List<Tuple<int, string, string, int, string, string>> GetQuestions() 
    {
        //Create SQL command to get all questions in Questions table.
        SQLiteDataReader dbDataReader;
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            SELECT QuestionID, Name, Description, Difficulty, Question, Answer
            FROM Questions
        ";

        //Declare the list of questions tuples
        List<Tuple<int, string, string, int, string, string>> questions = new List<Tuple<int, string, string, int, string, string>>();


        //Execute the SQL command on the database and assign the username, firstname, etc. to their corresponding variables.
        databaseConnection.Open();
        dbDataReader = databaseCMD.ExecuteReader();
        while (dbDataReader.Read())
        {
            int QuestionID = Convert.ToInt32(dbDataReader["QuestionID"]);
            string name = Convert.ToString(dbDataReader["Name"]);
            string description = Convert.ToString(dbDataReader["Description"]);
            int difficulty = Convert.ToInt32(dbDataReader["Difficulty"]);
            string question = Convert.ToString(dbDataReader["Question"]);
            string answer = Convert.ToString(dbDataReader["Answer"]);
            questions.Add(Tuple.Create<int, string, string, int, string, string> (QuestionID, name, description, difficulty, question, answer));
        }

        //Close data reader and database connection and return list of questions
        dbDataReader.Close();
        databaseConnection.Close();
        return questions;
    }

    public static void SubmitQuestionScore(int questionID, int accountID, int score, int maxScore)
    {
        //Create SQL command which creates a new record in the QuestionScores table
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            INSERT INTO QuestionsScores 
            (Question, Account, Score, MaxScore)
            VALUES 
            ($questionID, $accountID, $score, $maxScore)
        ";

        //Assign the parameters to protect against SQL injection
        databaseCMD.Parameters.AddWithValue("$questionID", questionID);
        databaseCMD.Parameters.AddWithValue("$accountID", accountID);
        databaseCMD.Parameters.AddWithValue("$score", score);
        databaseCMD.Parameters.AddWithValue("$maxScore", maxScore);

        //Open database connection, execute command then close it
        databaseConnection.Open();
        databaseCMD.ExecuteNonQuery();
        databaseConnection.Close();
    }

    static public string GetQuestionScore(int accountID, int questionID)
    {
        //Create SQL command to select any records with the passed accountID and questionID
        SQLiteDataReader dbDataReader;
        databaseCMD = databaseConnection.CreateCommand();
        databaseCMD.CommandText =
        @"
            SELECT  Score,MaxScore
            FROM QuestionsScores
            WHERE Account= $accountID and Question= $questionID 
        ";
        // Set username and password parameters (to protect against SQL injection) to username and password entered
        databaseCMD.Parameters.AddWithValue("$accountID", accountID);
        databaseCMD.Parameters.AddWithValue("$questionID", questionID);
        string score = "-1"; //Default -1 for when no records are found.

        //Open Database connection and execute the SQL command then close the connection
        databaseConnection.Open();
        dbDataReader = databaseCMD.ExecuteReader();
        while (dbDataReader.Read()) //Every iteration is for each record found
        {
            score = Convert.ToString(dbDataReader["Score"]) + "/" + dbDataReader["MaxScore"];
        }
        dbDataReader.Close();
        databaseConnection.Close();
        return score;
    }


}
