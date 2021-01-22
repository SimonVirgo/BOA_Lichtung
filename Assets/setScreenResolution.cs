using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setScreenResolution : MonoBehaviour
{

    void Start()
    {
        // Switch to 640 x 480 full-screen
        Screen.SetResolution(320, 168, true);
    }
}
