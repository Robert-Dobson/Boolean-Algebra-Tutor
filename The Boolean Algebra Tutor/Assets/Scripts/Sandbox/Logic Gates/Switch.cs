using System;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public bool state;
    public GameObject outputNode;
    static int numOfSwitches = 0;
    public string letter;

    //Sprites for each possible switch
    public Sprite spriteA;
    public Sprite spriteB;
    public Sprite spriteC;
    public Sprite spriteD;

    //Reference to error message for too many switches
    public GameObject errorMessage;
   
    public void OnTrue()
    {
        GetComponent<SpriteRenderer>().color = Color.green; //Change sprite to green
        outputNode.GetComponent<OutputNode>().OnTrue(); //Call OnTrue method on output node
    }

    public void OnFalse()
    {
        GetComponent<SpriteRenderer>().color = Color.red; //Change sprite to red
        outputNode.GetComponent<OutputNode>().OnFalse(); //Call OnFalse method on output node
    }

    private void OnMouseOver()
    {
        // If the user right clicks then reverse the state and call the corresponding method
        if (Input.GetMouseButtonDown(1))
        {
            state = !state;
            if (state == true)
            {
                OnTrue();
            }
            else
            {
                OnFalse();
            }

        }

        if (Input.GetKeyDown("d")) //If user presses d whilst hovering over the switch destroy it
        {
            outputNode.GetComponent<OutputNode>().DestroyWire();
            Destroy(gameObject);
            numOfSwitches -= 1;
        }
    }

    public void Start()
    {
        errorMessage = GameObject.FindWithTag("ErrorMessage"); //Get reference to error message game object
        numOfSwitches += 1; //Increment number of switches (static variable)
        if(numOfSwitches == 1) //If first switch make it switch A
        {
            letter = "A";
            GetComponent<SpriteRenderer>().sprite = spriteA;
        }
        else if (numOfSwitches == 2) //If second switch make it switch B
        {
            letter = "B";
            GetComponent<SpriteRenderer>().sprite = spriteB;
        }
        else if (numOfSwitches == 3) //If third switch make it switch C
        {
            letter = "C";
            GetComponent<SpriteRenderer>().sprite = spriteC;
        }
        else if (numOfSwitches == 4) //If fourth switch make it switch D
        {
            letter = "D";
            GetComponent<SpriteRenderer>().sprite = spriteD;
        }
        else
        {
            //Show error message that there's too many switches for a truth table then make it disapear after 10s
            errorMessage.GetComponent<Image>().enabled = true;
            errorMessage.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            Invoke("StopErrorMessage", 5f);
        }

        


        //Call OnFalse on first initialization to fix white logic gates bug
        OnFalse();
    }

    public void StopErrorMessage()
    {
        //Hide error message
        errorMessage.GetComponent<Image>().enabled = false;
        errorMessage.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
