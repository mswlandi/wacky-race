using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : CarController
{
    public float reachedTargetDistanceThreshold = 7f;
    public float checkpointsDistanceThreshold = 150f;

    public Checkpoints checkpoints;
    private int currentCheckpoint;

    public override void Update()
    {
        if (DriveTo(checkpoints.Transforms[currentCheckpoint]))
        {
            IncrementCheckpoint();
        }
    }

    private bool DriveTo(Transform targetTransform)
    {
        bool alreadyReached = false;

        float forwardAmount = 0f;
        float turnAmount = 0f;
        bool shouldBreak = false;

        Vector3 targetPosition = targetTransform.position;
        float distanceToTarget = Vector3.Distance(car.transform.position, targetPosition);
        float distanceFromCurrentToNextCheckpoint = Vector3.Distance(targetPosition, checkpoints.Transforms[(currentCheckpoint + 1) % checkpoints.Transforms.Length].position);
        Vector3 dirToMovePosition = (targetPosition - car.transform.position).normalized;
        float dot = Vector3.Dot(-targetTransform.forward, dirToMovePosition);
        float angleToDir = Vector3.SignedAngle(car.transform.forward, dirToMovePosition, Vector3.up);

        alreadyReached = distanceToTarget <= reachedTargetDistanceThreshold;

        if (!alreadyReached)
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
        
        car.Throttle = forwardAmount * (distanceFromCurrentToNextCheckpoint / checkpointsDistanceThreshold);
        car.Steer = turnAmount;
        car.ShouldBrake = shouldBreak;

        return alreadyReached;
    }

    private void IncrementCheckpoint()
    {
        currentCheckpoint = (currentCheckpoint + 1) % checkpoints.Transforms.Length;
    }
}
