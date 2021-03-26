using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float rotateSpeed = 180f;
    private float floatSpeed = 2f;
    private float floatAmplitude = 0.3f;

    private Transform lightningTransform;
    private Vector3 initialPosition;
    private int addUp = 20;

    // Start is called before the first frame update
    void Start()
    {
        lightningTransform = this.gameObject.transform.GetChild(0);
        initialPosition = lightningTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lightningTransform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        lightningTransform.position = initialPosition + new Vector3(0, floatAmplitude * Mathf.Sin(floatSpeed * Time.time), 0);
    }

    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if(player != null)
        {
            player.IncrementEnergy(addUp);
        }
    }
}
