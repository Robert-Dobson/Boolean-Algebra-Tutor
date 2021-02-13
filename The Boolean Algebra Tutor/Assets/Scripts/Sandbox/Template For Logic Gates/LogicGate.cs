using UnityEngine;

public class LogicGate : MonoBehaviour
{
    public bool state = false;
    public GameObject[] inputNodes = new GameObject[2];
    public GameObject outputNode;

    public virtual void OnTrue()
    {
        GetComponent<SpriteRenderer>().color = Color.green; //Change sprite to green
        outputNode.GetComponent<OutputNode>().OnTrue(); //Call OnTrue method on output node
    }

    public virtual void OnFalse()
    {
        GetComponent<SpriteRenderer>().color = Color.red; //Change sprite to red
        outputNode.GetComponent<OutputNode>().OnFalse(); //Call OnFalse method on output node
    }

    public virtual void OnChange()
    {
        //This method should be overidded in child classes
        Debug.Log("You forgot to overide OnChange()!");
    }

    public void Start()
    {
        //Call OnFalse on first initialization to fix white logic gates bug
        OnFalse();
    }

    public void OnMouseOver()
    {
        if (Input.GetKeyDown("d")) //If user presses d whilst hovering over the logic gate destroy it
        {
            // Remove any references to the game object by calling destroy on the input nodes.

            foreach (GameObject node in inputNodes)
            {
                if (node != null)
                {
                    node.GetComponent<InputNode>().Destroy();
                }

            }

            if (outputNode != null)
            {
                outputNode.GetComponent<OutputNode>().DestroyWire();
            }


            Destroy(gameObject);
        }
    }
}
