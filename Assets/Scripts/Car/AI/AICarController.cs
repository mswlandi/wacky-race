using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : CarController
{
    public float steerThreshold = 1f;
    public float distanceThreshold = 7f;
    public float speedAfterCatchThreshold = 15f;

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

        float reachedTargetDistance = distanceThreshold;
        float distanceToTarget = Vector3.Distance(car.transform.position, targetPosition);

        if (distanceToTarget > reachedTargetDistance)
        {
            Vector3 dirToMovePosition = (targetPosition - car.transform.position).normalized;

            float dot = Vector3.Dot(-targetPositionTransform.forward, dirToMovePosition);
            if (dot > 0)
            {
                forwardAmount = 0.5f;
            }
            else
            {
                forwardAmount = -0.5f;
            }

            float angleToDir = Vector3.SignedAngle(car.transform.forward, dirToMovePosition, Vector3.up);

            Debug.Log("Posição do carro: ");
            Debug.Log(car.transform.position);
            Debug.Log("Posição do target: ");
            Debug.Log(targetPosition);
            Debug.Log("Angulo pro target: ");
            Debug.Log(angleToDir);

            if (Mathf.Abs(angleToDir) > steerThreshold)
            {
                if (angleToDir > 0)
                {
                    turnAmount = 0.5f;
                }
                else
                {
                    turnAmount = -0.5f;
                }
            }    
        }
        else
        {
            // Already catched the target
            if (car.GetSpeed() > speedAfterCatchThreshold)
            {
                forwardAmount = -0.5f;
            }
            else
            {
                forwardAmount = 0f;
            }
            turnAmount = 0f;
            shouldBreak = true;
            currentCheckpoint = (currentCheckpoint + 1)%checkpoints.Transforms.Length;
        }
        
        car.Throttle = forwardAmount;
        car.Steer = turnAmount;
        car.ShouldBrake = shouldBreak;
    }

    public void SetTargetPosition(Transform targetTransform)
    {
        this.targetPositionTransform = targetTransform;
        this.targetPosition = targetTransform.TransformPoint(Vector3.zero);
    }
}
