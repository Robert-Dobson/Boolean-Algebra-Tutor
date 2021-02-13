public class XORGate : LogicGate
{
    public override void OnChange()
    {
        //Calls OnTrue if only one of the inputs nodes are true, else calls OnFalse
        if (inputNodes[0].GetComponent<InputNode>().state && inputNodes[1].GetComponent<InputNode>().state == false)
        {
            OnTrue();
        }
        else if (inputNodes[0].GetComponent<InputNode>().state == false && inputNodes[1].GetComponent<InputNode>().state == true)
        {
            OnTrue();
        }
        else
        {
            OnFalse();
        }
    }
}
