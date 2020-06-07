using UnityEngine;

public class Wire : MonoBehaviour
{
    private bool state = false;
    public GameObject outputNode;
    private GameObject connectedNode;
    private bool fixedWire;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collided with an input node and wire not fixed and the input node is not connected to another wire.
        if (fixedWire == false && collision.gameObject.CompareTag("InputNode") && collision.gameObject.GetComponent<InputNode>().isFixed == false) 
        {
            connectedNode = collision.gameObject; 
            if (connectedNode.transform.parent.gameObject != outputNode.transform.parent.gameObject) //If input node is not its own logic gate input node
            {
                if (state == true) //If wire is logically true then call OnTrue() on connected input node 
                {
                    connectedNode.GetComponent<InputNode>().OnTrue();
                }
                else //Else call OnFalse() on connected input node
                {
                    connectedNode.GetComponent<InputNode>().OnFalse();
                }
                //Set fixed wire flags on output node, input node and this object to true
                outputNode.GetComponent<OutputNode>().fixedWire = true;
                connectedNode.GetComponent<InputNode>().isFixed = true;
                GetComponent<EdgeCollider2D>().enabled = false;
                fixedWire = true;

                //Add reference to the output node to the input node so it can alert the output node if it's about to be deleted
                connectedNode.GetComponent<InputNode>().connectedNode = outputNode;

            }  
        }
    }


    private void Update()
    {
        
        // If the wire is connected to another input node then make the wire always visually connect them 
        if (fixedWire)
        {
            GetComponent<LineRenderer>().SetPosition(0, outputNode.transform.position);
            GetComponent<LineRenderer>().SetPosition(1, connectedNode.transform.position);
        }
    }

    public void OnTrue()
    {
        // Set state to true and call on true function on connected node if connected.
        state = true;
        if (fixedWire)
        {
            connectedNode.GetComponent<InputNode>().OnTrue();
        }
    }

    public void OnFalse()
    {
        // Set state to false and call on false function on connected node if connected.
        state = false;
        if (fixedWire)
        {
            connectedNode.GetComponent<InputNode>().OnFalse();
        }
    }

    public void Destroy()
    {
        // Set fixed wire to false and make wire invisible.
        fixedWire = false;
        GetComponent<LineRenderer>().enabled = false;
        GetComponent<EdgeCollider2D>().enabled = false;

        //Remove references to the output node and also set fixed wire to false and call OnFals on the connected input node 
        if (connectedNode != null)
        {     
            connectedNode.GetComponent<InputNode>().isFixed = false;
            connectedNode.GetComponent<InputNode>().OnFalse();
            connectedNode.GetComponent<InputNode>().connectedNode = null;
        }
    }
}
