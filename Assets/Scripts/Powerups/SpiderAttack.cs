using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttack : MonoBehaviour
{
    public float radius;
    public float force;

    private Rigidbody rigidbody;
    private Player player;
    private SphereCollider collider;
    List <Rigidbody> currentRigidBodyColliding = new List <Rigidbody>();

    private bool exploding = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        player = GetComponentInParent<Player>();
        collider = GetComponent<SphereCollider>();

        collider.radius = radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space) && isAvailableToAttack(player))
        {
            Attack();
            if (player.CompareTag("Player")) FindObjectOfType<AudioManager>().Play("Power");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody otherRigidBody = other.GetComponentInParent<Rigidbody>();
        if(otherRigidBody != null)
        {
            currentRigidBodyColliding.Add(otherRigidBody);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody otherRigidBody = other.GetComponentInParent<Rigidbody>();
        if(otherRigidBody != null)
        {
            currentRigidBodyColliding.Remove(otherRigidBody);
        }
    }

    bool isAvailableToAttack(Player player)
    {
        return player.Energy.value == 100;
    }

    void DeactivateAttack()
    {
        player.DecrementEnergy(player.Energy.value);
    }

    void Attack()
    {
        foreach (Rigidbody rigidbody in currentRigidBodyColliding)
        {
            rigidbody.AddExplosionForce(force, player.Position, radius);
        }

        DeactivateAttack();
    }
}
