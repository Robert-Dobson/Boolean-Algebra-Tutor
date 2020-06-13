using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    //References to username and password text boxes
    public GameObject usernameTextBox;
    public GameObject passwordTextBox;

    //Reference to error label;
    public GameObject errorLabel;

    public void OnLogin()
    {
        // Get the entered username and password.
        string enteredUsername = usernameTextBox.GetComponent<TMP_InputField>().text;
        string enteredPassword = passwordTextBox.GetComponent<TMP_InputField>().text;

        //Convert entered Password to hash for comparision in database
        enteredPassword = Sha256(enteredPassword);
        
        //Get the associated accountID for these entered credentials (or -1 if the credentials is incorrect)
        int accountID = DBManager.CheckCredentials(enteredUsername, enteredPassword);
        
        // If correct credentials then call CorrectCredentials() else call IncorrectCredentials()
        if (accountID != -1)
        {
            CorrectCredentials(accountID);
        }
        else
        {
            IncorrectCredentials();
        }

    }

    public string Sha256(string plainText)
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

    public void CorrectCredentials(int accountID)
    {
        //Update the user details on the UserManager class
        DBManager.GetUserDetails(accountID);

        //If the user is a student redirect them to the student switchboard,
        //If the user is a teacher redirect them to the teacher switchboard.
        if (UserManager.accountType == 0)
        {
            SceneManager.LoadScene("Student Switchboard");
        }
        else
        {
            SceneManager.LoadScene("Teacher Switchboard");
        }
        
    }

    public void IncorrectCredentials()
    {
        errorLabel.GetComponent<TextMeshProUGUI>().text = "Incorrect!";
    }
}
