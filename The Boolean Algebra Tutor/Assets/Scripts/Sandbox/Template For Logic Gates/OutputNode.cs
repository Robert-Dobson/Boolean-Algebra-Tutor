using UnityEngine;

public class OutputNode : MonoBehaviour
{
    private bool state = false;
    public GameObject wire;
    public bool fixedWire = false;

    public void OnTrue()
    {
        // Make state true and change colour of game object to green
        state = true;
        GetComponent<SpriteRenderer>().color = Color.green;
        wire.GetComponent<Wire>().OnTrue(); //Call OnTrue method on connected wire
    }

    public void OnFalse()
    {
        // Make state false and change colour of game object to red
        state = false;
        GetComponent<SpriteRenderer>().color = Color.red;
        wire.GetComponent<Wire>().OnFalse(); //Call OnFalse method on connected wire

    }

    public void OnMouseDrag()
    {
        if (!fixedWire)
        {
            LineRenderer lineRenderer = wire.GetComponent<LineRenderer>();
            lineRenderer.enabled = true; //Make the wire visible whilst dragging on the output node
            UnityEngine.Vector3 outputNodePosition = GetComponent<Transform>().position;
            outputNodePosition.z = 0; // Makes z coordinate 0 to avoid any visual artefacts
            lineRenderer.SetPosition(0, outputNodePosition); //Make wire start at the output node
            UnityEngine.Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Returns the world coordinates for the mouse.
            mousePosition.z = 0; // Makes z coordinate 0 to avoid any visual artefacts.
            lineRenderer.SetPosition(1, mousePosition); //Make end of wire at mouse

            EdgeCollider2D edgeCollider = wire.GetComponent<EdgeCollider2D>();
            edgeCollider.enabled = true; //Enabe the collider for the wire
            UnityEngine.Vector2[] vectors = new UnityEngine.Vector2[2];
            //The edge collider uses local coordinates rather than world coordinates so the following code uses (0,0) (position of the output node)
            //as the starting coordinate and minus the world coordinates of the input node from the mouse position in order to get the local coordinates
            //equivalent of the mouse position. Also, I have scaled the mouse position coordinates by a factor in 4 in order to nullify the bug where the
            //edge collider is a quarter of the intended size.
            vectors[0] = new UnityEngine.Vector2 (0f, 0f); 
            vectors[1] = new UnityEngine.Vector2(4*(mousePosition.x - outputNodePosition.x), 4*(mousePosition.y - outputNodePosition.y)); 
            edgeCollider.points = vectors; //Set start and end coordinates as the output node positon and the mouse.
        }
    }

    public void OnMouseUp()
    {
        if (!fixedWire)
        {
            // Make wire invisible and have no collider after letting go of the mouse
            wire.GetComponent<LineRenderer>().enabled = false; 
            wire.GetComponent<EdgeCollider2D>().enabled = false;
        }
    }
    private void OnMouseOver()
    {
        // If right click on the wire and wire is fixed.
        if (Input.GetMouseButtonDown(1) && fixedWire)
        {
            //Set fixed wire flags to false on this script and call the destroy method on the wire.
            fixedWire = false;
            wire.GetComponent<Wire>().Destroy();
        }
    }
}
