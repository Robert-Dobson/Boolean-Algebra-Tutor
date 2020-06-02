using System;
using UnityEngine;
using UnityEngine.UI;

public class TruthTableGenerator : MonoBehaviour
{
    private string truthTable;
    public GameObject truthTableString;

    public void GenerateTruthTable()
    {
        //Get all switch game objects and bulb game objects and assign to an array
        GameObject[] switches = GameObject.FindGameObjectsWithTag("Switch");
        GameObject[] bulb = GameObject.FindGameObjectsWithTag("Bulb");

        //Validation
        if (bulb.Length != 1) //Truth table only supports 1 bulb 
        {
            truthTableString.GetComponent<Text>().text = "Too many bulbs";
        }
        else if (switches.Length <= 1 || switches.Length > 4) //Truth Table only supports 2-4 switches
        {
            truthTableString.GetComponent<Text>().text = "There must only be 2 to 4 switches";
        }
        else
        {
            truthTable = ""; //Remove previous truth table data
            //Sort the array so then switch A is first, switch B is second etc.
            GameObject[] tempArray = switches; //Uses temp array so then I can adjust the real array values
            foreach (GameObject aSwitch in tempArray)
            {
                if (aSwitch.GetComponent<Switch>().letter == "A")
                {
                    switches[0] = aSwitch;
                }
                else if (aSwitch.GetComponent<Switch>().letter == "B")
                {
                    switches[1] = aSwitch;
                }
                else if (aSwitch.GetComponent<Switch>().letter == "C")
                {
                    switches[2] = aSwitch;
                }
                else if (aSwitch.GetComponent<Switch>().letter == "D")
                {
                    switches[3] = aSwitch;
                }
            }


            for (int i=0; i < Math.Pow(2, switches.Length) ; i++)
            {
                //Loop through every combination of inputs
                string countString = Convert.ToString(i, 2); //Adds up in binary so then every possible set of inputs is covered (logic gates work in binary)
                countString = countString.PadLeft(switches.Length, '0'); //Adds leading 0's so string is always switches.length long (represents each input)
                for (int j=0; j < switches.Length; j++)
                {
                    //Access each input, gets the character in countString which represents the input's state and sets state appropriately
                    char state = countString[j];
                    if (state == '1')
                    {
                        switches[j].GetComponent<Switch>().OnTrue();
                    }
                    else
                    {
                        switches[j].GetComponent<Switch>().OnFalse();
                    }
                }

                truthTable += Convert.ToString(Convert.ToInt32(bulb[0].GetComponent<Bulb>().state)); //Add bulb's state to Truth Table
            }

            truthTableString.GetComponent<Text>().text = truthTable;
        }


    }
}
