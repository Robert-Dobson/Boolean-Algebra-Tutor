using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    //Stores all user details as static attributes so the rest of the program can use it.
    static public int accountID;
    static public string userName;
    static public string firstName;
    static public string lastName;
    static public int accountType; //0 if student, 1 if teacher
    
    static public void UpdateUser(int newAccountID, string newUserName, string newFirstName, string newLastName, int newAccountType)
    {
        //Update user details
        accountID = newAccountID;
        userName = newUserName;
        firstName = newFirstName;
        lastName = newLastName;
        accountType = newAccountType;
    }
    
}
