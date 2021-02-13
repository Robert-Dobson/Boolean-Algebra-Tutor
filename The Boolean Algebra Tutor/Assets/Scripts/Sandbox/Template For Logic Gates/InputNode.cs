using UnityEngine;

public class InputNode : MonoBehaviour
{
    public bool state = false;
    public bool isFixed = false;
    public GameObject connectedNode;

    public void OnTrue()
    {
        // Change state to true and colour to green
        state = true;
        GetComponent<SpriteRenderer>().color = Color.green;
        transform.parent.gameObject.GetComponent<LogicGate>().OnChange(); //Call OnChange Function on parent game object.
    }

    public void OnFalse()
    {
        //Change state to false and colour to red
        state = false;
        GetComponent<SpriteRenderer>().color = Color.red;
        transform.parent.gameObject.GetComponent<LogicGate>().OnChange(); // Call OnChange Function on parent game object.
    }

    public void Start()
    {
        //Call OnFalse on first initialization to fix white logic gates bug
        OnFalse();
    }

    public void Destroy()
    {
        //Call destroy wire function on connected node 
        if (connectedNode != null)
        {
            connectedNode.GetComponent<OutputNode>().DestroyWire();
        }

    }
}
