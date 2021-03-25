using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CarController
{
    public override void Update()
    {
        car.Steer = GameManager.Instance.InputController.SteerInput;
        car.Throttle = GameManager.Instance.InputController.ThrottleInput;
        car.ShouldBrake = GameManager.Instance.InputController.BrakeInput;
    }
}
