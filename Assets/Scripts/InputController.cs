using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public string inputSteerAxis = "Horizontal";
    public string inputThrottleAxis = "Vertical";

    public float ThrottleInput { get; set; }
    public float SteerInput { get; set; }
    public bool BrakeInput { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SteerInput = Input.GetAxis(inputSteerAxis);
        ThrottleInput = Input.GetAxis(inputThrottleAxis);
        BrakeInput = Input.GetKey(KeyCode.Space);
    }
}
