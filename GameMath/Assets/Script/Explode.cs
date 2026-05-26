using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explode : MonoBehaviour
{
    public float force = 300f;

    public float radius = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("RunExplode", 2f);
    }

    void RunExplode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (var col in hitColliders)
        {
            Rigidbody rb = col.attachedRigidbody;
            if (rb != null)
            {
                rb.AddExplosionForce(force, explosionPos, radius);
            }
        }
        Destroy(gameObject);
    }
}
