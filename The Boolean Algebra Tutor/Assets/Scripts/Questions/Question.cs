using System;
using TMPro;
using UnityEngine;

public class Question : MonoBehaviour
{
    //Stores question, answer and questionID (for database)
    //As static so then these can be directly modified by the question selector
    static public int questionID;
    static public string question;
    static public string answer;

    //Users answer
    public string userAnswer;

    //Reference to labels and pop-ups
    public GameObject confirmationPopUp;
    public GameObject resultPopUp;
    public GameObject questionLabel;
    public GameObject scoreLabel;

    public void Start()
    {
        //Change question label to the question selected.
        questionLabel.GetComponent<TextMeshProUGUI>().text = question;

    }

    public void Confirm(string truthTable)
    {
        //Store the users answer and show confirmation pop up
        userAnswer = truthTable;
        confirmationPopUp.SetActive(true);
    }

    public void SubmitAnswer()
    {
        //Work out the score by adding 1 to the score for each number the same in truth table string
        int score = 0;
        for (int i = 0; i < answer.Length; i++)
        {
            //Try catch clause in case the user answer is smaller than the actual answer truth
            // table string if they didn't use enough switches 
            try
            {
                if (answer[i] == userAnswer[i])
                {
                    score++;
                }
            }
            catch
            {

            }

        }

        //Show the score on the scorelabel and show the result pop up menu
        scoreLabel.GetComponent<TextMeshProUGUI>().text = "You Scored " + Convert.ToString(score) + "/" + Convert.ToString(answer.Length);
        resultPopUp.SetActive(true);

        //Store Question Score in the QuestionScores table.
        DBManager.SubmitQuestionScore(questionID, UserManager.accountID, score, answer.Length);

    }
}
