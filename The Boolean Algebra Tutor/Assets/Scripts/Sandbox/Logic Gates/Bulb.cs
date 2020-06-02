using UnityEngine;

public class Bulb : LogicGate
{
    public override void OnTrue()
    {
        // Make state false and sprite green
        state = true;
        GetComponent<SpriteRenderer>().color = Color.green; 
    }

    public override void OnFalse()
    {
        // Make state false and sprite red
        state = false;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public override void OnChange()
    {
        // Get the state of the input node and then call OnTrue() if true or OnFalse() if false.
        bool inputNodeState = inputNodes[0].GetComponent<InputNode>().state;
        if (inputNodeState)
        {
            OnTrue();
        }
        else
        {
            OnFalse();
        }
    }

    
}
