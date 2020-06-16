﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignUpManager : MonoBehaviour
{
    //References to textboxes, error labels and dropdown box
    public GameObject firstNameTextBox;
    public GameObject firstNameErrorLabel;
    public GameObject lastNameTextBox;
    public GameObject lastNameErrorLabel;
    public GameObject typeAccountDropBox;
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
        firstNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;
        lastNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;
        passwordErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;
        
        //ValidationError flag checks if any of the details failed their validation
        bool validationError = false;

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

        //Get Type of Account, the drop down box already has validation by limiting
        //The possible inputs to Student or Teacher so not needed here.
        //If student then account type boolean is 0, if teacher it is 1.
        int accountType = typeAccountDropBox.GetComponent<TMP_Dropdown>().value;
       
        //Get passwords and check if they match otherwise hash it.
        string password = "";
        string password1 = password1TextBox.GetComponent<TMP_InputField>().text;
        string password2 = password2TextBox.GetComponent<TMP_InputField>().text;
        if (password1 != password2)
        {
            validationError = true;
            //Show passwords do not match error message
            passwordErrorLabel.GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else
        {

            //Hash it
            password = DBManager.Sha256(password1);
        }

        //If there is not a validation error create account
        if (!validationError)
        {
            // Call create account on DBManager
            DBManager.CreateAccount(firstName, lastName, accountType, password);

            //Return to login page
            SceneManager.LoadScene("Login Screen");
        }

    }
    
    
}