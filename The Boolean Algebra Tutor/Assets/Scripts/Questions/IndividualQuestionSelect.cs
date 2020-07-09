using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class IndividualQuestionSelect : MonoBehaviour
{
    //Holds references to each label on question in grid
    public GameObject nameLabel;
    public GameObject descriptionLabel;
    public GameObject difficultyLabel;

    //Holds all the question data required to give the user the question
    private int questionID;
    private string questionName;
    private string description;
    private int difficulty;
    private string question;
    private string answer;
    
    public void UpdateQuestion(Tuple<int, string, string, int, string, string> details)
    {
        //Update question details stored in variables
        questionID = details.Item1;
        questionName = details.Item2;
        description = details.Item3;
        difficulty = details.Item4;
        question = details.Item5;
        answer = details.Item6;

        //Update labels on question in grid
        nameLabel.GetComponent<TextMeshProUGUI>().text = questionName;
        descriptionLabel.GetComponent<TextMeshProUGUI>().text = description;
        difficultyLabel.GetComponent<TextMeshProUGUI>().text = Convert.ToString(difficulty);
    }

    public void LoadQuestion()
    {
        //Update question details in the sandbox to the question details of the question selected
        Question.questionID = questionID;
        Question.question = question;
        Question.answer = answer;

        //Load the answering questions sandbox
        SceneManager.LoadScene("Answer Question");
    }
   
   
}
