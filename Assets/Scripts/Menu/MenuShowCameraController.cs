using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShowCameraController : MonoBehaviour
{
    public Camera[] cameras;
    MenuButtonController controller;
    [SerializeField] int enabledCameraIndex = 0;
    public bool activated = false;

    public int EnabledCameraIndex {
        get => enabledCameraIndex;
        set {
            if (activated && value >= 0 && value < cameras.Length)
            {
                cameras[enabledCameraIndex].enabled = false;
                cameras[value].enabled = true;
                enabledCameraIndex = value;
            }
        }
    }

    void Start()
    {
        controller = GetComponentInParent<MenuButtonController>();
    }

    void Update()
    {
        EnabledCameraIndex = controller.index;
    }

    
}
