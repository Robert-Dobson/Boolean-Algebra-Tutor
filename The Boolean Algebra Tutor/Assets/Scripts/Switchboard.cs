using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switchboard : MonoBehaviour
{
   public void ToSandbox()
   {
        //Load up sandbox
        SceneManager.LoadScene("Sandbox");
    }

    public void ToLogin()
    {
        //Load up login screen
        SceneManager.LoadScene("Login Screen");
    }
}
