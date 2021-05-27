using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    public Camera currentEnabledCamera;

    public Camera CurrentCamera {
        get => currentEnabledCamera;
        set {
            currentEnabledCamera.enabled = false;
            value.enabled = true;
            currentEnabledCamera = value;
        }
    }
}
