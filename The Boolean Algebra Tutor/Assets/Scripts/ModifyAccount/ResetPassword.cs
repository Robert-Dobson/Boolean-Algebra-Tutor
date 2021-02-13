using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPassword : MonoBehaviour
{
    //References to textboxes and pop up labels/messages
    public GameObject passwordTextBox1;
    public GameObject passwordTextBox2;
    public GameObject passwordErrorMessage;
    public GameObject passwordSuccessMessage;

    public void SubmitPassword()
    {
        //Hide previous pop up messages
        passwordErrorMessage.GetComponent<TextMeshProUGUI>().enabled = false;
        passwordSuccessMessage.GetComponent<TextMeshProUGUI>().enabled = false;

        //Get the users inputs
        string password1 = passwordTextBox1.GetComponent<TMP_InputField>().text;
        string password2 = passwordTextBox2.GetComponent<TMP_InputField>().text;

        //If passwords do not match
        if (password1 != password2)
        {
            //Show not matching error message and stop
            passwordErrorMessage.GetComponent<TextMeshProUGUI>().text = "Passwords do not Match!";
            passwordErrorMessage.GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else if (password1 == "")
        {
            //Show error message for user to enter password then stop
            passwordErrorMessage.GetComponent<TextMeshProUGUI>().text = "You must enter a password!";
            passwordErrorMessage.GetComponent<TextMeshProUGUI>().enabled = true;

        }

        //If password does not fit the security validation (detialed in method)
        else if (!(DBManager.CheckPasswordStrength(password1)))
        {
            //Show password is insecure enough error message
            passwordErrorMessage.GetComponent<TextMeshProUGUI>().text = "Password is insecure!";
            passwordErrorMessage.GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else
        {
            //Hash the entered password for database 
            string password = DBManager.Sha256(password1);

            //Change the password in record to new password by calling method in DBManager
            DBManager.ResetPassword(UserManager.accountID, password);

            //Show success message
            passwordSuccessMessage.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }

    public void ToModify()
    {
        SceneManager.LoadScene("Modify Account");
    }
}
