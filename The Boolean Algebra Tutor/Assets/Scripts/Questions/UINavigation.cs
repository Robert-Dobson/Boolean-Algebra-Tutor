﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINavigation : MonoBehaviour
{
    public void ToSwitchboard()
    {
        SceneManager.LoadScene("Switchboard");
    }
}
