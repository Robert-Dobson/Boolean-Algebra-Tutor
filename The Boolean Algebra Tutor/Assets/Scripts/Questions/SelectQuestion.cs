using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectQuestion : MonoBehaviour
{
    public GameObject questionPrefab;

    void Start()
    {
        Populate();
    }

    private void Populate()
    {
        //Get all the questions from the database
        List<Tuple<int, string, string, int, string, string>> questions = DBManager.GetQuestions();

        //Instantiate a list of tuples for the already answered questions with scores.
        List<Tuple<int, string, string, int, string, string, string>> completedQuestions = new List<Tuple<int, string, string, int, string, string, string>>();

        //Fill the grid with these questions by going through each element and instantiating a question prefab.
        foreach (Tuple<int, string, string, int, string, string> aQuestion in questions)
        {
            //Get users most recent score on the question or "-1" if not answered
            string score = DBManager.GetQuestionScore(UserManager.accountID, aQuestion.Item1);

            //If the user hasn't attempted the question yet add the question to the list
            if (score == "-1")
            {
                //Instantiate the question prefab (the element in the grid)
                GameObject newQuestion = Instantiate(questionPrefab, transform);

                //Call Update Question on the instinated question to give it all the question details.
                newQuestion.GetComponent<IndividualQuestionSelect>().UpdateQuestion(aQuestion);
            }
            //If the user has attempted the question before add it to the new completedQuestions list.
            else
            {
                completedQuestions.Add(Tuple.Create<int, string, string, int, string, string, string>(aQuestion.Item1, aQuestion.Item2, aQuestion.Item3, aQuestion.Item4, aQuestion.Item5, aQuestion.Item6, score));

            }
        }

        //Add any completed questions at the bottom of the list
        foreach (Tuple<int, string, string, int, string, string, string> completedQuestion in completedQuestions)
        {
            //Instantiate the question prefab (the element in the grid)
            GameObject newQuestion = Instantiate(questionPrefab, transform);

            //Call Update Question With Score on the instantiated question to give it all the question details and score.
            newQuestion.GetComponent<IndividualQuestionSelect>().UpdateQuestionWithScore(completedQuestion);
        }
    }


}






