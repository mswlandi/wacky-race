using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{

    public RectTransform crosshair;
    public float rotationSnapTime = 0.2F;

    public float minScale = 0.4f;
    public float maxScale = 2f;
    public float maxScaleAtDistance = 0;
    public float minScaleAtDistance = 20;

    public int enemiesLayerMask = 11;

    public float plungerSpeed = 50F;

    private Transform target;
    private float yVelocity = 0.0F;
    private float xVelocity = 0.0F;

    private Transform rotatableBase;
    private Transform rotatableArm;
    private Transform plunger;
    private Quaternion targetRotation;

    private bool isAvailableToFire = true;
    private bool isBeingLaunched = false;

    private PlungerStick plungerStickCollisions;
    private Rigidbody enemyRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rotatableBase = transform.GetChild(0);
        rotatableArm = rotatableBase.GetChild(0);
        plunger = rotatableArm.GetChild(0);

        plungerStickCollisions = plunger.GetComponent<PlungerStick>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Find Target
        if (isAvailableToFire)
        {
            SetTargetCloserInFront();
        }
        #endregion

        #region Look-At Rotations
        if (isAvailableToFire)
        {
            targetRotation = Quaternion.LookRotation(target.transform.position - rotatableBase.position);
            rotatableBase.eulerAngles = new Vector3(0, Mathf.SmoothDampAngle(rotatableBase.eulerAngles.y, targetRotation.eulerAngles.y, ref yVelocity, rotationSnapTime), 0);

            targetRotation = Quaternion.LookRotation(rotatableArm.position - target.transform.position);
            rotatableArm.eulerAngles = new Vector3(Mathf.SmoothDampAngle(rotatableArm.eulerAngles.x, targetRotation.eulerAngles.x, ref xVelocity, rotationSnapTime), 180 + rotatableBase.eulerAngles.y, 0);
        }
        #endregion

        #region Make Cross-Hair Follow target
        if (Camera.main.WorldToScreenPoint(target.position).z > 0 && isAvailableToFire)
        {
            crosshair.position = Camera.main.WorldToScreenPoint(target.position);

            float distance = Vector3.Distance(Camera.main.transform.position, target.position);
            float distance_t = Mathf.InverseLerp(minScaleAtDistance, maxScaleAtDistance, distance);
            float scale = Mathf.Lerp(minScale, maxScale, distance_t);

            crosshair.localScale = new Vector3(scale, scale, scale);
        }
        else
        {
            crosshair.localScale = new Vector3(0, 0, 0);
        }
        #endregion

        #region Firing
        if (Input.GetKeyDown (KeyCode.Space) && isAvailableToFire)
        {
            isAvailableToFire = false;
            isBeingLaunched = true;
            plunger.parent = null;
        }
        #endregion

        // Reactivate Power-Up (CHANGE METHOD OF ACTIVATION)
        if (Input.GetKeyDown (KeyCode.Tab))
        {
            isAvailableToFire = true;
            isBeingLaunched = false;
            plunger.parent = rotatableArm;

            plungerStickCollisions.isAttached = false;
            plungerStickCollisions.isAttachedToCar = false;

            plunger.localPosition = new Vector3(0, 0.396291F, -1.7468F);
            plunger.localEulerAngles = new Vector3(0, 0, 0);
        }

        #region Is Being Launched (it's airborne)
        if (isBeingLaunched)
        {
            if (!plungerStickCollisions.isAttached)
            {
                plunger.position = Vector3.MoveTowards(plunger.position, target.position, plungerSpeed * Time.deltaTime);
                plunger.rotation = Quaternion.LookRotation(plunger.position - target.transform.position);
            }
            else
            {
                plunger.parent = plungerStickCollisions.collidedObject.transform;

                if (plungerStickCollisions.isAttachedToCar)
                {
                    float force = 500;
                    enemyRigidBody = plungerStickCollisions.collidedObject.transform.parent.GetComponent<Rigidbody>();
                    enemyRigidBody.AddForce((transform.position - plungerStickCollisions.collidedObject.transform.position).normalized * force, ForceMode.Impulse);
                }
            }
        }
        #endregion
    }

    public bool SetTargetCloserInFront()
    {
        int layerMask = 1 << enemiesLayerMask;

        Collider[] CloseColliders = Physics.OverlapSphere (transform.position, 1000, layerMask);
        float sqrMinDistance = 100000;
        Transform ret = target;

        foreach (Collider Close in CloseColliders)
        {
            //set a if here, to check if the target is a foe
            float sqrDistance = (Close.transform.position - transform.position).sqrMagnitude;
            if (sqrDistance < sqrMinDistance && isInFront(Close.transform, transform))
            {
                sqrMinDistance = sqrDistance;
                ret = Close.transform;
            }
        }

        if (ret != target) {
            target = ret;
            return true;
        } 
        
        return false;
    }

    public bool isInFront(Transform target, Transform origin)
    {
        Vector3 heading = (target.position - origin.position).normalized;
        return Vector3.Dot(heading, origin.forward) > 0;
    }
}
