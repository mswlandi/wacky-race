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
    public float plungerForceTime = 0.2F;
    public float plungerForce = 500F;

    public Material lineMaterial;

    public Transform initialTarget;

    private Transform target;
    private float yVelocity = 0.0F;
    private float xVelocity = 0.0F;

    private Transform rotatableBase;
    private Transform rotatableArm;
    private Transform plunger;
    private Quaternion targetRotation;

    private bool isBeingLaunched = false;

    private PlungerStick plungerStickCollisions;
    private Rigidbody enemyRigidBody;

    Player player;

    private float currentPlungerTime = 0F;

    public bool enableCrosshair = true;

    // Start is called before the first frame update
    void Start()
    {
        rotatableBase = transform.GetChild(0);
        rotatableArm = rotatableBase.GetChild(0);
        plunger = rotatableArm.GetChild(0);

        plungerStickCollisions = plunger.GetComponent<PlungerStick>();
        player = plunger.GetComponentInParent<Player>();

        if (crosshair == null)
            enableCrosshair = false;

        target = initialTarget;
    }

    // Update is called once per frame
    void Update()
    {
        #region Calculate a couple positions
        Vector3 armTip = rotatableArm.position + 0.40F * rotatableArm.up - 0.6F * rotatableArm.forward;
        Vector3 plungerTip = plunger.position + 1.8F * plunger.forward;
        #endregion

        #region Find Target
        SetTargetCloserInFront();
        #endregion

        #region Look-At Rotations
        if (isAvailableToFire(player))
        {
            targetRotation = Quaternion.LookRotation(target.transform.position - rotatableBase.position);
            rotatableBase.eulerAngles = new Vector3(0, Mathf.SmoothDampAngle(rotatableBase.eulerAngles.y, targetRotation.eulerAngles.y, ref yVelocity, rotationSnapTime), 0);

            targetRotation = Quaternion.LookRotation(rotatableArm.position - target.transform.position);
            rotatableArm.eulerAngles = new Vector3(Mathf.SmoothDampAngle(rotatableArm.eulerAngles.x, targetRotation.eulerAngles.x, ref xVelocity, rotationSnapTime), 180 + rotatableBase.eulerAngles.y, 0);
        }
        #endregion

        if (enableCrosshair)
        {
            #region Make Cross-Hair Follow target
            if (Camera.main.WorldToScreenPoint(target.position).z > 0 && isAvailableToFire(player))
            {
                crosshair.gameObject.SetActive(true);
                crosshair.position = Camera.main.WorldToScreenPoint(target.position);

                float distance = Vector3.Distance(Camera.main.transform.position, target.position);
                float distance_t = Mathf.InverseLerp(minScaleAtDistance, maxScaleAtDistance, distance);
                float scale = Mathf.Lerp(minScale, maxScale, distance_t);

                crosshair.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                crosshair.gameObject.SetActive(false);
            }
            #endregion
        }

        #region Firing
        if (Input.GetKeyDown (KeyCode.Space) && isAvailableToFire(player))
        {
            if (player.CompareTag("Player")) FindObjectOfType<AudioManager>().Play("Power");
            player.DecrementEnergy(100);
            isBeingLaunched = true;
            plunger.parent = null;
        }
        #endregion

        // Reactivate Power-Up (CHANGE METHOD OF ACTIVATION)
        if (Input.GetKeyDown (KeyCode.Tab))
        {
            ResetPlunger();
            player.IncrementEnergy(100); // Temporary for testing purposes, it should stay at zero
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

                
                enemyRigidBody = plungerStickCollisions.collidedObject.transform.parent.GetComponent<Rigidbody>();
                if (currentPlungerTime < plungerForceTime)
                {
                    if (enemyRigidBody != null)
                    {
                        enemyRigidBody.AddForce((transform.position - plungerStickCollisions.collidedObject.transform.position).normalized * plungerForce, ForceMode.Impulse);
                    }
                }
                else
                {
                    plunger.position = Vector3.MoveTowards(plunger.position, armTip, plungerSpeed * Time.deltaTime);
                    plunger.rotation = Quaternion.LookRotation(plunger.position - target.transform.position);

                    if (plungerStickCollisions.collidedWithPlungerArm)
                    {
                        ResetPlunger();
                    }
                }

                currentPlungerTime += Time.deltaTime;
            }

            DrawLine(armTip, plungerTip, Color.white);
        }
        #endregion
    }
    
    void ResetPlunger()
    {
        isBeingLaunched = false;
        plunger.parent = rotatableArm;

        plungerStickCollisions.isAttached = false;
        plungerStickCollisions.isAttachedToCar = false;
        plungerStickCollisions.collidedWithPlungerArm = false;

        currentPlungerTime = 0F;

        plunger.localPosition = new Vector3(0, 0.396291F, -1.7468F);
        plunger.localEulerAngles = new Vector3(0, 0, 0);
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.03f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = lineMaterial;
        lr.textureMode = LineTextureMode.Tile;
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }



    public bool SetTargetCloserInFront()
    {
        int layerMask = 1 << enemiesLayerMask;

        Collider[] CloseColliders = Physics.OverlapSphere (transform.position, 1000, layerMask);
        float sqrMinDistance = 100000;
        Transform ret = target;

        foreach (Collider Close in CloseColliders)
        {
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

    public bool isAvailableToFire(Player player)
    {
        if (player.Energy.value == 100)
        {
            return true;
        }
        
        return false;
    }
}
