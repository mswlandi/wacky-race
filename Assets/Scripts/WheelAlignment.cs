using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WheelAlignment : MonoBehaviour
{
    public WheelCollider correspondingCollider;
    private float rotationVel = 0F;

    public void Update()
    {
        // Hit point for the raycast collision
        RaycastHit hit;
        
        // Find the collider's center point. The center of the collider might not actually be 
        //   the real position if the transform's off.
        Vector3 colliderCenter = correspondingCollider.transform.TransformPoint(correspondingCollider.center);

        // Cast a ray out from the wheel collider's center the distance of the suspension, 
        // if it hit something, then use the "hit" variable's data to find where the wheel hit, 
        // if it didn't, then se tthe wheel to be fully extended along the suspension.
        bool collided = Physics.Raycast(colliderCenter, 
                                        -correspondingCollider.transform.up,
                                        out hit,
                                        correspondingCollider.suspensionDistance + correspondingCollider.radius);
        if (collided)
        {
            transform.position = hit.point + (correspondingCollider.transform.up * correspondingCollider.radius);
        }
        else
        {
            transform.position = colliderCenter - (correspondingCollider.transform.up * correspondingCollider.suspensionDistance);
        }

        // Set the wheel rotation to the rotation of the collider combined with a new rotation value. This new value
        // is the rotation around the axle, and the rotation from steering input.
        transform.rotation = correspondingCollider.transform.rotation * Quaternion.Euler(rotationVel, correspondingCollider.steerAngle, 0);

        // Increase the rotation value by the rotation speed (in degrees per second)
        rotationVel += correspondingCollider.rpm * (360 / 60) * Time.deltaTime;
    }

}