using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CreateQuestion : MonoBehaviour
{
    //References to metdadata inputs and error labels gameobjects (for create question setup)
    public GameObject questionNameInput;
    public GameObject questionNameErrorLabel;
    public GameObject questionDescriptionInput;
    public GameObject questionDescriptionErrorLabel;
    public GameObject difficutlySlider;

    //References to confirmation popup, inptus and error label gameobjects (for create question sandbox)
    public GameObject confirmationPopUp;
    public GameObject questionInput;
    public GameObject errorLabel;

    //Static variables which stores the questions metadata (between scenes)
    public static string questionName;
    public static string questionDescription;
    public static int difficulty;
    public static string answerTruthTable;
    public static string question;

    //Called to update the metadata variables and move to sandbox
    public void SubmitMetadata()
    {
        //Flag which checks if the validation tests have passed or not
        bool validationError = false;

        //Hide all previous error labels
        questionNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;
        questionDescriptionErrorLabel.GetComponent<TextMeshProUGUI>().enabled = false;

        //Get question name
        questionName = questionNameInput.GetComponent<TMP_InputField>().text;
        //Validation: Length Check <=30 
        if (questionName.Length > 30)
        {
            //Make validation flag true and show error label
            validationError = true;
            questionNameErrorLabel.GetComponent<TextMeshProUGUI>().text = "Question Name too long!";
            questionNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = true;
        }
        //Validation: Presence Check
        if (questionName.Length == 0)
        {
            //Make validation flag true and show error label
            validationError = true;
            questionNameErrorLabel.GetComponent<TextMeshProUGUI>().text = "Question Name is required";
            questionNameErrorLabel.GetComponent<TextMeshProUGUI>().enabled = true;
        }

        //Get question description
        questionDescription = questionDescriptionInput.GetComponent<TMP_InputField>().text;
        //Validation: Length Check <=100
        if (questionDescription.Length > 100)
        {
            //Make validation flag true and show error label
            validationError = true;
            questionDescriptionErrorLabel.GetComponent<TextMeshProUGUI>().text = "Question description too long!";
            questionDescriptionErrorLabel.GetComponent<TextMeshProUGUI>().enabled = true;
        }
        //Validation: Presence Check
        if (questionDescription.Length == 0)
        {
            //Make validation flag true and show error label
            validationError = true;
            questionDescriptionErrorLabel.GetComponent<TextMeshProUGUI>().text = "Question description is required";
            questionDescriptionErrorLabel.GetComponent<TextMeshProUGUI>().enabled = true;
        }

        //Get Question difficulty
        difficulty = Convert.ToInt32(difficutlySlider.GetComponent<Slider>().value);

        //If all validation checks have passed
        if (!(validationError))
        {
            //Move onto Sandbox for creating questions to enter question and answer
            SceneManager.LoadScene("Create Question");
        }
    }

    public void PrepareSubmission(string truthTable)
    {
        //Assign truth table answer to passed truth table argument
        answerTruthTable = truthTable;

        //Flag which checks if the validation tests have passed or not
        bool validationError = false;

        //Get question entered and complete a length check of <=250.
        question = questionInput.GetComponent<TMP_InputField>().text;
        if (question.Length > 250)
        {
            validationError = true;
            errorLabel.GetComponent<TextMeshProUGUI>().text = "Question too long!";
        }
        //Vaidation: Presence Check
        if (question.Length == 0)
        {
            validationError = true;
            errorLabel.GetComponent<TextMeshProUGUI>().text = "Question is required";
        }

        //If all validation tests passed then show confirmation popup
        if (!(validationError))
        {
            confirmationPopUp.SetActive(true);
        }

    }

    public void Submit()
    {
        //Add question to database
        DBManager.CreateQuestion(questionName, questionDescription, difficulty, question, answerTruthTable, UserManager.accountID);

        //Return to switchboard
        SceneManager.LoadScene("Switchboard");
    }

    public void ReturnToSetup()
    {
        SceneManager.LoadScene("Create Setup");
    }

    public void ReturnToSwitchboard()
    {
        SceneManager.LoadScene("Switchboard");
    }
}
