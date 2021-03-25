using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 1500f;
    public float minimumSpeed = 10f;
    public float maxSteer = 20f;
    public float brakeForce = 300f;
    public float engineBrakeForce = 100f;

    public float Steer { get; set; }
    public float Throttle { get; set; }

    protected float currentSpeed;

    private bool ShouldBrake { get; set; }

    private Rigidbody _rigidbody;
    private Wheel[] wheels;
    
    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void Update()
    {
        Steer = GameManager.Instance.InputController.SteerInput;
        Throttle = GameManager.Instance.InputController.ThrottleInput;
        ShouldBrake = GameManager.Instance.InputController.BrakeInput;

        currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;

        foreach(var wheel in wheels)
        {
            if(ShouldBrake)
            {
                Debug.Log("freia porra");
                wheel.BrakeTorque = brakeForce;
            }
            else 
            {
                if (Throttle == 0 && currentSpeed > minimumSpeed)
                {
                    Debug.Log("freia");
                    wheel.BrakeTorque = engineBrakeForce;
                }
                else
                {
                    Debug.Log("acelera");
                    wheel.Torque = Throttle * motorTorque;
                    wheel.BrakeTorque = 0;
                }
            }

            Debug.Log(currentSpeed);
            wheel.SteerAngle = Steer * maxSteer;
        }
    }
}
