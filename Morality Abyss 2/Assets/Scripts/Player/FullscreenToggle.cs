using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{
    public void Fullscreen(bool fs)
    {
        Screen.fullScreen = fs;
    }
}
