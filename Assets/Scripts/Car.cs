using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    public Transform centerOfMass;

    public WheelCollider wheelColliderLeftFront;
    public WheelCollider wheelColliderRightFront;
    public WheelCollider wheelColliderLeftBack;
    public WheelCollider wheelColliderRightBack;

    public Transform wheelLeftFront;
    public Transform wheelRightFront;
    public Transform wheelLeftBack;
    public Transform wheelRightBack;

    public float motorTorque = 100f;
    public float maxSteer = 20f;
    public float brakeTorque = 20f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical") * motorTorque;
        float h = Input.GetAxis("Horizontal") * maxSteer;

        wheelColliderLeftBack.motorTorque = v;
        wheelColliderRightBack.motorTorque = v;

        wheelColliderLeftFront.steerAngle = h;
        wheelColliderRightFront.steerAngle = h;

        if(Input.GetKey(KeyCode.Space))
        {
            wheelColliderLeftBack.brakeTorque = brakeTorque;
            wheelColliderRightBack.brakeTorque = brakeTorque;
        }
    }

    void Update()
    {
        var pos = Vector3.zero;
        var rot = Quaternion.identity;

        wheelColliderLeftFront.GetWorldPose(out pos, out rot);
        wheelLeftFront.position = pos;
        wheelLeftFront.rotation = rot * Quaternion.Euler(0, 90, 90);

        wheelColliderRightFront.GetWorldPose(out pos, out rot);
        wheelRightFront.position = pos;
        wheelRightFront.rotation = rot * Quaternion.Euler(0, 90, 90);

        wheelColliderLeftBack.GetWorldPose(out pos, out rot);
        wheelLeftBack.position = pos;
        wheelLeftBack.rotation = rot * Quaternion.Euler(0, 90, 90);

        wheelColliderRightBack.GetWorldPose(out pos, out rot);
        wheelRightBack.position = pos;
        wheelRightBack.rotation = rot * Quaternion.Euler(0, 90, 90);
    }
}
