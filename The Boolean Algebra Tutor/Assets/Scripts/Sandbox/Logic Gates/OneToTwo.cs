using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneToTwo : LogicGate
{
     public GameObject inputNode;
     public GameObject[] outputNodes = new GameObject[2];

    public override void OnTrue()
    {
        GetComponent<SpriteRenderer>().color = Color.green; //Change sprite to green
        outputNodes[0].GetComponent<OutputNode>().OnTrue(); //Call OnTrue method on output node 1
        outputNodes[1].GetComponent<OutputNode>().OnTrue(); //Call OnTrue method on output node 2
    }
    public override void OnFalse()
    {
        GetComponent<SpriteRenderer>().color = Color.red; //Change sprite to red
        outputNodes[0].GetComponent<OutputNode>().OnFalse(); //Call OnFalse method on output node 1
        outputNodes[1].GetComponent<OutputNode>().OnFalse(); //Call OnFalse method on output node 2
    }

    public override void OnChange()
    {
        //Calls OnTrue if input is true, else calls OnFalse
        if (inputNode.GetComponent<InputNode>().state == true )
        {
            OnTrue();
        }
        else
        {
            OnFalse();
        }
    }

}
