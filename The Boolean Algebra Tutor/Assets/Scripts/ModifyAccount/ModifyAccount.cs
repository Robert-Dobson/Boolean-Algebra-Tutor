using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModifyAccount : MonoBehaviour
{
    //References to textboxes, error labels and dropdown box
    public GameObject usernameTextBox;
    public GameObject usernameErrorLabel;
    public GameObject firstNameTextBox;
    public GameObject firstNameErrorLabel;
    public GameObject lastNameTextBox;
    public GameObject lastNameErrorLabel;
    public GameObject successMessage;

    private void Start()
    {
        ShowAccountDetails();
    }

    private void ShowAccountDetails()
    {
        //Show the current user details on the modify your account page
        usernameTextBox.GetComponent<TMP_InputField>().text = UserManager.userName;
        firstNameTextBox.GetComponent<TMP_InputField>().text = UserManager.firstName;
        lastNameTextBox.GetComponent<TMP_InputField>().text = UserManager.lastName;
    }

    public void Modify()
    {
        //Hide all error labels
        usernameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;
        firstNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;
        lastNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;

        //ValidationError flag checks if any of the details failed their validation
        bool validationError = false;

        //Get username, Length Check validation must be <=15 and Presence check
        string username = usernameTextBox.GetComponent<TMP_InputField>().text;
        if (username.Length > 15 || username.Length < 1)
        {
            validationError = true;
            //Show username error message that username is invalid
            usernameErrorLabel.GetComponent<TextMeshProUGUI>().text = "Invalid Username";
            usernameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = true;
        }

        // If username is already in the Accounts Table in the database tell user username is already used
        else if (DBManager.UserExists(username) && !(username == UserManager.userName))
        {
            validationError = true;
            //Show username error message that username is already used
            usernameErrorLabel.GetComponent<TextMeshProUGUI>().text = "Username already used";
            usernameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = true;
        }

        //Get First Name, Length Check validation must be <=15 and Presence Check
        string firstName = firstNameTextBox.GetComponent<TMP_InputField>().text;
        if (firstName.Length > 15 || firstName.Length < 1)
        {
            validationError = true;
            //Show first name error message
            firstNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = true;
        }

        //Get Last Name, Length Check validation must be <=15 and Presence Check
        string lastName = lastNameTextBox.GetComponent<TMP_InputField>().text;
        if (lastName.Length > 15 || lastName.Length < 1)
        {
            validationError = true;
            //Show last name error message
            lastNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = true;
        }

        if (!validationError)
        {
            //Update the account details in database
            DBManager.UpdateAccount(UserManager.accountID, username, firstName, lastName);

            //Get new user details from database and update the UserManager class
            Tuple<int, string, string, string> userDetails = DBManager.GetUserDetails(UserManager.accountID);
            UserManager.UpdateUser(userDetails);

            //Update fields with new user details
            ShowAccountDetails();

            //Display to user a 5s modification successful message
            successMessage.GetComponent<TextMeshProUGUI>().enabled = true;
            Invoke("HideSuccessMessage", 5f);

        }
    }

    public void HideSuccessMessage()
    {
        successMessage.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    public void ToSwitchboard()
    {
        //Load Switchboard
        SceneManager.LoadScene("Switchboard");
    }

    public void ToReset()
    {
        SceneManager.LoadScene("Reset Password");
    }

    public void DeleteAccount()
    {
        //Call DBManager DeleteAccount to delete the account off the database
        DBManager.DeleteAccount(UserManager.accountID);

        //Return user to login screen which in turn sign the user out.
        SceneManager.LoadScene("Login Screen");
    }
}
