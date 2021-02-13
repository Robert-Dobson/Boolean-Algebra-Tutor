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

    public void ToModify()
    {
        //Load up Modify Account page
        SceneManager.LoadScene("Modify Account");
    }

    public void ToCreate()
    {
        //Load up create setup page
        SceneManager.LoadScene("Create Setup");
    }

    public void ToQuestions()
    {
        SceneManager.LoadScene("Select Question");
    }

    public void ToNotes()
    {
        SceneManager.LoadScene("Notes");
    }
}
