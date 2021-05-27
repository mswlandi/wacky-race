using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttack : MonoBehaviour
{
    public float radius;
    public float force;
    public float duration = 4;

    private Rigidbody rigidbody;
    private Player player;
    private SphereCollider collider;

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
            StartCoroutine(Attack());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody otherRigidBody = other.GetComponentInParent<Rigidbody>();
        if(otherRigidBody != null && exploding)
        {
            otherRigidBody.AddExplosionForce(force, player.Position, radius);
        }
    }

    bool isAvailableToAttack(Player player)
    {
        return player.Energy.value == 100;
    }

    void DeactivateAttack()
    {
        player.DecrementEnergy(player.Energy.value);
        exploding = false;
    }

    IEnumerator Attack()
    {
        exploding = true;

        yield return new WaitForSeconds(duration);

        DeactivateAttack();
    }
}
