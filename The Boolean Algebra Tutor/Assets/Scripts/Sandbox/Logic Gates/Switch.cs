using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool state;
    public GameObject outputNode;
    static int numOfSwitches = 0;
    public string letter;

    //Sprites for each possible switch
    public Sprite spriteA;
    public Sprite spriteB;
    public Sprite spriteC;
    public Sprite spriteD;
   
   
    public void OnTrue()
    {
        GetComponent<SpriteRenderer>().color = Color.green; //Change sprite to green
        outputNode.GetComponent<OutputNode>().OnTrue(); //Call OnTrue method on output node
    }

    public void OnFalse()
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
            numOfSwitches -= 1;
        }
    }

    public void Start()
    {
        numOfSwitches += 1; //Increment number of switches (static variable)
        if(numOfSwitches == 1) //If first switch make it switch A
        {
            letter = "A";
            GetComponent<SpriteRenderer>().sprite = spriteA;
        }
        else if (numOfSwitches == 2) //If second switch make it switch B
        {
            letter = "B";
            GetComponent<SpriteRenderer>().sprite = spriteB;
        }
        else if (numOfSwitches == 3) //If third switch make it switch C
        {
            letter = "C";
            GetComponent<SpriteRenderer>().sprite = spriteC;
        }
        else if (numOfSwitches == 4) //If fourth switch make it switch D
        {
            letter = "D";
            GetComponent<SpriteRenderer>().sprite = spriteD;
        }


        //Call OnFalse on first initialization to fix white logic gates bug
        OnFalse();
    }
}
