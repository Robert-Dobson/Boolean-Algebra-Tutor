﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ModifyAccount : MonoBehaviour
{
    //References to textboxes, error labels and dropdown box
    public GameObject firstNameTextBox;
    public GameObject firstNameErrorLabel;
    public GameObject lastNameTextBox;
    public GameObject lastNameErrorLabel;
    public GameObject typeAccountDropBox;
    public GameObject successMessage;

    private void Start()
    {
        ShowAccountDetails();
    }

    private void ShowAccountDetails()
    {
        //Show the current user details on the modify your account page
        firstNameTextBox.GetComponent<TMP_InputField>().text = UserManager.firstName;
        lastNameTextBox.GetComponent<TMP_InputField>().text = UserManager.lastName;
        typeAccountDropBox.GetComponent<TMP_Dropdown>().value = UserManager.accountType;
    }

    public void Modify()
    {
        //Hide all error labels
        firstNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;
        lastNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;

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

        if (!validationError)
        {
            //Update the account details in database
            DBManager.UpdateAccount(UserManager.accountID, firstName, lastName, accountType);

            //Get new user details from database and update the UserManager class
            Tuple<int, string, string, string, int> userDetails = DBManager.GetUserDetails(UserManager.accountID);
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
        if (UserManager.accountType == 0) //If user is a student
        {
            SceneManager.LoadScene("Student Switchboard"); //Load student switchboard
        }
        else //If user is a teacher
        {
            SceneManager.LoadScene("Teacher Switchboard"); //Load teacher switchboard
        }
    }
}