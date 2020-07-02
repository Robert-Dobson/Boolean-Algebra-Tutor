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
    
    static public void UpdateUser(Tuple<int, string, string, string> userDetails)
    {
        //Update user details
        accountID = userDetails.Item1;
        userName = userDetails.Item2;
        firstName = userDetails.Item3;
        lastName = userDetails.Item4;
    }
    
}
