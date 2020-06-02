public class NOTGate : LogicGate
{
    public override void OnChange()
    {
        // Outputs the opposite to the input
        if (inputNodes[0].GetComponent<InputNode>().state == true)
        {
            OnFalse();
        }
        else
        {
            OnTrue();
        }
    }
}
