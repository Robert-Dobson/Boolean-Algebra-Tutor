using UnityEngine;

public class ORGate : LogicGate
{
    public override void OnChange()
    {
        // If either input is true
        if (inputNodes[0].GetComponent<InputNode>().state == true|| inputNodes[1].GetComponent<InputNode>().state == true)
        {
            OnTrue();
        }
        else
        {
            OnFalse();
        }
    }
}
