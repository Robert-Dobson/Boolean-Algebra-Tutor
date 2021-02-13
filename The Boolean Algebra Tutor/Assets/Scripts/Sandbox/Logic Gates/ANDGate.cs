public class ANDGate : LogicGate
{
    public override void OnChange()
    {
        //Calls OnTrue if both the input nodes are true, else calls OnFalse
        if (inputNodes[0].GetComponent<InputNode>().state == true && inputNodes[1].GetComponent<InputNode>().state == true)
        {
            OnTrue();
        }
        else
        {
            OnFalse();
        }
    }
}
