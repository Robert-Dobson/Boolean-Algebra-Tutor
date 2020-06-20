using System;
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
    
    static public void UpdateUser(Tuple<int, string, string, string, int> userDetails)
    {
        //Update user details
        accountID = userDetails.Item1;
        userName = userDetails.Item2;
        firstName = userDetails.Item3;
        lastName = userDetails.Item4;
        accountType = userDetails.Item5;
    }
    
}
