using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TruthTableGenerator : MonoBehaviour
{
    private string truthTable;
    public GameObject truthTableString;

    //References to TruthTables and their text elements
    public GameObject truthTable2;
    public GameObject[] text2 = new GameObject[4];
    public GameObject truthTable3;
    public GameObject[] text3 = new GameObject[8];
    public GameObject truthTable4;
    public GameObject[] text4 = new GameObject[16];

    public void GenerateTruthTable()
    {
        //Get all switch game objects and bulb game objects and assign to an array
        GameObject[] switches = GameObject.FindGameObjectsWithTag("Switch");
        GameObject[] bulb = GameObject.FindGameObjectsWithTag("Bulb");

        //Disable all truth tables to prevent overlapping 
        truthTable2.SetActive(false);
        truthTable3.SetActive(false);
        truthTable4.SetActive(false);

        //Reset information text so then error messages do not persist
        truthTableString.GetComponent<Text>().text = "";
        
        //Validation
        if (bulb.Length != 1) //Truth table only supports 1 bulb 
        {
            truthTableString.GetComponent<Text>().text = "There must only be 1 bulb";
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

            //Store an array representing all the orignal states of the switches so then we can keep their values
            bool[] switchesStates = new bool [switches.Length];
            for (int i=0; i < switches.Length; i++)
            {
                switchesStates[i] = switches[i].GetComponent<Switch>().state;
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

            //Restore orignal states of switches
            for (int i = 0; i < switches.Length; i++)
            {
                if (switchesStates[i])
                {
                    switches[i].GetComponent<Switch>().OnTrue();
                }
                else
                {
                    switches[i].GetComponent<Switch>().OnFalse();
                }
            }

            //Call correct fill truth table method depdning on number of switches
            if (switches.Length == 2)
            {
                FillTruthTable2();
            }
            else if (switches.Length == 3)
            {
                FillTruthTable3();
            }
            else if (switches.Length == 4)
            {
                FillTruthTable4();
            }
            else
            {
                truthTableString.GetComponent<Text>().text = truthTable;
            }
            


        }


    }

    public void FillTruthTable2()
    {
        truthTable2.SetActive(true);
        for (int i=0; i < truthTable.Length; i++)
        {
            text2[i].GetComponent<TextMeshProUGUI>().text = Convert.ToString(truthTable[i]);
        }
    }

    public void FillTruthTable3()
    {
        truthTable3.SetActive(true);
        for (int i=0; i< truthTable.Length; i++)
        {
            text3[i].GetComponent<TextMeshProUGUI>().text = Convert.ToString(truthTable[i]);
        }
    }

    public void FillTruthTable4()
    {
        truthTable4.SetActive(true);
        for (int i = 0; i < truthTable.Length; i++)
        {
            text4[i].GetComponent<TextMeshProUGUI>().text = Convert.ToString(truthTable[i]);
        }
    }
}
