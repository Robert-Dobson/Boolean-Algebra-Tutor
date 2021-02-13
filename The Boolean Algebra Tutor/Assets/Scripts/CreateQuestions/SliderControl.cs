using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    public GameObject label;
    public void OnChange()
    {
        //Update label to show the value of the slider
        float value = this.GetComponent<Slider>().value;
        label.GetComponent<TextMeshProUGUI>().text = Convert.ToString(value);
    }
}
