using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Question : MonoBehaviour
{
    //Stores question, answer and questionID (for database)
    //As static so then these can be directly modified by the question selector
    static public int questionID;
    static public string question;
    static public string answer;

    //Reference to question label
    public GameObject questionLabel;
    
    public void Start()
    {
        //Change question label to the question selected.
        questionLabel.GetComponent<TextMeshProUGUI>().text = question;
    }
}
