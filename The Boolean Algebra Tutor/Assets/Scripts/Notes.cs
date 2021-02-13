using UnityEngine;
using UnityEngine.SceneManagement;

public class Notes : MonoBehaviour
{
    //Reference to all game objects holding the notes
    public GameObject overviewNote;
    public GameObject andGateNote;
    public GameObject orGateNote;
    public GameObject notGateNote;
    public GameObject xorGateNote;

    public void HideAll()
    {
        //Hide all notes so then they don't overlap
        overviewNote.SetActive(false);
        andGateNote.SetActive(false);
        orGateNote.SetActive(false);
        notGateNote.SetActive(false);
        xorGateNote.SetActive(false);
    }

    public void ShowOverview()
    {
        //Show overview notes
        HideAll();
        overviewNote.SetActive(true);
    }

    public void ShowOrGate()
    {
        //Show OR Gate notes
        HideAll();
        orGateNote.SetActive(true);
    }

    public void ShowAndGate()
    {
        //Show AND Gate notes
        HideAll();
        andGateNote.SetActive(true);
    }

    public void ShowNotGate()
    {
        //Show NOT Gate notes
        HideAll();
        notGateNote.SetActive(true);
    }

    public void ShowXORGate()
    {
        //Show XOR Gate notes
        HideAll();
        xorGateNote.SetActive(true);
    }

    public void ToSwitchboard()
    {
        SceneManager.LoadScene("Switchboard");
    }
}