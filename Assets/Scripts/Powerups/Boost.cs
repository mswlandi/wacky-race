using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private Rigidbody rb;
    private float thrust = 500000f;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.V) && isAvailableToFire(player))
        {
            rb.AddRelativeForce(Vector3.forward * thrust);
            player.DecrementEnergy(50);
        }
    }

     public bool isAvailableToFire(Player player)
    {
        if (player.Energy.value > 50)
        {
            return true;
        }
        
        return false;
    }
}
