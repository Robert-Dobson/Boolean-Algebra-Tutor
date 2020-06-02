using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private void OnMouseDrag()
    {
        // Make the position of the switch follow the mouse when dragging
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Returns the world coordinates for the mouse.
        GetComponent<Transform>().position = mousePosition;
    }

}
