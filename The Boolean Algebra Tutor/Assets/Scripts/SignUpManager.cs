using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignUpManager : MonoBehaviour
{
    //References to textboxes, error labels and dropdown box
    public GameObject usernameTextBox;
    public GameObject usernameErrorLabel;
    public GameObject firstNameTextBox;
    public GameObject firstNameErrorLabel;
    public GameObject lastNameTextBox;
    public GameObject lastNameErrorLabel;
    public GameObject password1TextBox;
    public GameObject password2TextBox;
    public GameObject passwordErrorLabel;


    public void ToLogin()
    {
        SceneManager.LoadScene("Login Screen");
    }

    public void Signup()
    {
        //Hide all error labels
        usernameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;
        firstNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;
        lastNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;
        passwordErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;

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
        else if (DBManager.UserExists(username))
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

        //Get passwords and check if they match otherwise check strength and hash
        string password = "";
        string password1 = password1TextBox.GetComponent<TMP_InputField>().text;
        string password2 = password2TextBox.GetComponent<TMP_InputField>().text;
        if (password1 != password2)
        {
            validationError = true;
            //Show passwords do not match error message
            passwordErrorLabel.GetComponent<TextMeshProUGUI>().text = "Passwords do not match!";
            passwordErrorLabel.GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else
        {
            //If password is fits the security validation (detialed in method)
            if (DBManager.CheckPasswordStrength(password1))
            {
                //Hash it
                password = DBManager.Sha256(password1);
            }
            else
            {
                validationError = true;
                //Show password is insecure enough error message
                passwordErrorLabel.GetComponent<TextMeshProUGUI>().text = "Password is insecure!";
                passwordErrorLabel.GetComponent<TextMeshProUGUI>().enabled = true;
            }

        }

        //If there is not a validation error create account
        if (!validationError)
        {
            // Call create account on DBManager
            DBManager.CreateAccount(username, firstName, lastName, password);

            //Return to login page
            SceneManager.LoadScene("Login Screen");
        }

    }


}
