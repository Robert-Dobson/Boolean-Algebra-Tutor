using System.Numerics;
using UnityEngine;

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

    //Methods below all add the logic gate which corresponds to each button in adding gates section.
    public void AddSwitch()
    {
        Instantiate(switchPrefab);
    }

    public void AddBulb()
    {
        Instantiate(bulbPrefab);
    }

    public void AddANDGate()
    {
        Instantiate(andGatePrefab);
    }

    public void AddORGate()
    {
        Instantiate(orGatePrefab);
    }

    public void AddNOTGate()
    {
        Instantiate(notGatePrefab);
    }

    public void AddXORGate()
    {
        Instantiate(xorGatePrefab);
    }

}
