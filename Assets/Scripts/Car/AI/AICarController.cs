using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : CarController
{
    public float reachedTargetDistanceThreshold = 7f;
    public float checkpointsDistanceThreshold = 150f;

    public Checkpoints checkpoints;
    private int currentCheckpoint;

    private Vector3 targetPosition;
    private Transform targetPositionTransform;

    public override void Update()
    {
        SetTargetPosition(checkpoints.Transforms[currentCheckpoint]);

        float forwardAmount = 0f;
        float turnAmount = 0f;
        bool shouldBreak = false;

        float distanceToTarget = Vector3.Distance(car.transform.position, targetPosition);
        float distanceFromCurrentToNextCheckpoint = Vector3.Distance(targetPosition, checkpoints.Transforms[(currentCheckpoint + 1) % checkpoints.Transforms.Length].position);
        Vector3 dirToMovePosition = (targetPosition - car.transform.position).normalized;
        float dot = Vector3.Dot(-targetPositionTransform.forward, dirToMovePosition);
        float angleToDir = Vector3.SignedAngle(car.transform.forward, dirToMovePosition, Vector3.up);

        if (distanceToTarget > reachedTargetDistanceThreshold)
        {
            if (dot > 0)
            {
                forwardAmount = 1f;
            }
            else
            {
                forwardAmount = -1f;
            }

            turnAmount = angleToDir / 180;
        }
        else
        {
            currentCheckpoint = (currentCheckpoint + 1) % checkpoints.Transforms.Length;
        }
        
        car.Throttle = forwardAmount * (distanceFromCurrentToNextCheckpoint / checkpointsDistanceThreshold);
        car.Steer = turnAmount;
        car.ShouldBrake = shouldBreak;
    }

    public void SetTargetPosition(Transform targetTransform)
    {
        this.targetPositionTransform = targetTransform;
        this.targetPosition = targetTransform.position;
    }
}
