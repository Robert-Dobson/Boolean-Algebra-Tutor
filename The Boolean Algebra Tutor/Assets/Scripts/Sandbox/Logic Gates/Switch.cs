using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool state;
    public GameObject outputNode;
   
   
    private void OnTrue()
    {
        GetComponent<SpriteRenderer>().color = Color.green; //Change sprite to green
        outputNode.GetComponent<OutputNode>().OnTrue(); //Call OnTrue method on output node
    }

    private void OnFalse()
    {
        GetComponent<SpriteRenderer>().color = Color.red; //Change sprite to red
        outputNode.GetComponent<OutputNode>().OnFalse(); //Call OnFalse method on output node
    }

    private void OnMouseOver()
    {
        // If the user right clicks then reverse the state and call the corresponding method
        if (Input.GetMouseButtonDown(1))
        {
            state = !state;
            if (state == true)
            {
                OnTrue();
            }
            else
            {
                OnFalse();
            }

        }

        if (Input.GetKeyDown("d")) //If user presses d whilst hovering over the switch destroy it
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        //Call OnFalse on first initialization to fix white logic gates bug
        OnFalse();
    }
}
