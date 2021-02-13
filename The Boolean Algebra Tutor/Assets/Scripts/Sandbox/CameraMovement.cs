using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float panSpeed = 10f; // Controls speed of panning
    private float panBorder = 10f; //Controls how close to the edge of the screen you must be to pan
    private UnityEngine.Vector2 panLimit = new UnityEngine.Vector2(20f, 20f); //Top left corner of pan limit


    void Update()
    {
        UnityEngine.Vector3 newPos = transform.position;

        if (Input.mousePosition.y >= Screen.height - panBorder)
        {
            newPos.y += panSpeed * Time.deltaTime; //Move newPos upwards (uses deltaTime so pan speed is constant regardless of fps
        }

        if (Input.mousePosition.y <= panBorder)
        {
            newPos.y -= panSpeed * Time.deltaTime; //Move newPos downwards
        }

        if (Input.mousePosition.x >= Screen.width - panBorder)
        {
            newPos.x += panSpeed * Time.deltaTime; //Move newPos to the right
        }

        if (Input.mousePosition.x <= panBorder)
        {
            newPos.x -= panSpeed * Time.deltaTime; //Move newPos to left
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float zoom = GetComponent<Camera>().orthographicSize - scroll * panSpeed * 75 * Time.deltaTime; //Zoom in and out when user is scrolling
        zoom = Mathf.Clamp(zoom, 3f, 14.28f); //Limit zoom to minimum of 3 and maximum of 14.28 (to avoid visual glitches)
        GetComponent<Camera>().orthographicSize = zoom;

        newPos.x = Mathf.Clamp(newPos.x, -panLimit.x, panLimit.x); //Limit x coordinate to within the positive and negative panLimit x coordinate
        newPos.y = Mathf.Clamp(newPos.y, -panLimit.y, panLimit.y); //Limit y coordinate to within the positive and negative panLimit y coordinate
        transform.position = newPos;

    }
}
