using System;

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
        enteredPassword = DBManager.Sha256(enteredPassword);

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



    public void CorrectCredentials(int accountID)
    {
        //Get user details from database and update the UserManager class
        Tuple<int, string, string, string> userDetails = DBManager.GetUserDetails(accountID);
        UserManager.UpdateUser(userDetails);

        //Redirect to switchboard
        SceneManager.LoadScene("Switchboard");
    }

    public void IncorrectCredentials()
    {
        errorLabel.GetComponent<TextMeshProUGUI>().text = "Incorrect!";
    }

    public void ToSignUp()
    {
        SceneManager.LoadScene("Sign up Screen");
    }
}
