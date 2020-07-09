using System;
using System.Collections;
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

        //Fill the grid with these questions by going through each element and instantiating a question prefab.
        foreach (Tuple<int, string, string, int, string, string> aQuestion in questions)
        {
            //Instantiate the question prefab (the element in the grid)
            GameObject newQuestion = Instantiate(questionPrefab, transform);
            
            //Call Update Question on the instinated question to give it all the question details.
            newQuestion.GetComponent<IndividualQuestionSelect>().UpdateQuestion(aQuestion);
        }
    }

   
}






