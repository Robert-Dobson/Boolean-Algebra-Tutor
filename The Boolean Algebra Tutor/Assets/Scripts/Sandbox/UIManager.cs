using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class UIManager : MonoBehaviour
{
    // Reference to left and right UI panels
    public GameObject leftPanel;
    public GameObject rightPanel;

    // Reference to each logic gate prefab for adding to scene via code
    public GameObject switchPrefab;
    public GameObject bulbPrefab;
    public GameObject andGatePrefab;
    public GameObject orGatePrefab;
    public GameObject notGatePrefab;
    public GameObject xorGatePrefab;

    //Reference to camera
    public GameObject mainCamera;

    public void LeftPanelView() //Called by Gates Button
    {
        if (leftPanel.activeSelf) //If left panel is visible
        {
            leftPanel.SetActive(false); //Make left panel invisible
        }
        else
        {
            leftPanel.SetActive(true); //Make left panel visible
        }
        
    }

    public void RightPanelView() //Called by Truth Table Button
    {
        if (rightPanel.activeSelf) //If right panel is visible
        {
            rightPanel.SetActive(false); //Make right panel invisible
        }
        else
        {
            rightPanel.SetActive(true); //Make right panel visible
        }

    }

    //Methods below all add the logic gate in centre of screen which corresponds to each button in adding gates section.
    public void AddSwitch()
    {
        SpawnLogicGate(switchPrefab);
    }

    public void AddBulb()
    {
        SpawnLogicGate(bulbPrefab);
    }

    public void AddANDGate()
    {
        SpawnLogicGate(andGatePrefab);
    }

    public void AddORGate()
    {
        SpawnLogicGate(orGatePrefab);
    }

    public void AddNOTGate()
    {
        SpawnLogicGate(notGatePrefab);
    }

    public void AddXORGate()
    {
        SpawnLogicGate(xorGatePrefab);
    }

    public void SpawnLogicGate(GameObject prefab)
    {
        Vector3 spawnLocation = mainCamera.transform.position;
        spawnLocation.z = 0f;

        //Ensure it doesn't instantiate on top of a logic gate.
        while (true) //until the logic gate does not overlap another.
        {
            Collider2D colliders = Physics2D.OverlapCircle(spawnLocation, 2f); //Returns any nearby colliders within a radius of 2
            if (colliders == null || spawnLocation.x >= 20f) //If there's no colliders nearby or if we are about to exit the game area.
            {
                break;
            }
            else
            {
                //Move the logic gate one unit to the right to prevent overlapping
                spawnLocation.x += 1f;
            }
        }
        // Instantiate the logic gate at spawn location.
        Instantiate(prefab, spawnLocation, mainCamera.transform.rotation);
    }

    public void ReturnToSwitchboard()
    {
        SceneManager.LoadScene("Switchboard");
    }

    //Reset number of switches as soon as the sandbox starts
    public void Awake()
    {
        Switch.numOfSwitches = 0;
    }
}
