using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Placeable turret;
    Rigidbody rb;

    public float damageAmount = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetTurret(Placeable placeable)
    {
        turret = placeable;
    }

    public void Launch(Vector3 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            return;
        }
    }
}
