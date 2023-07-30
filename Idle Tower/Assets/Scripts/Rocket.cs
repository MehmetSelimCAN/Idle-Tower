using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Rocket : MonoBehaviour
{
    private Rigidbody rb;
    private float rocketSpeed = 200f;
    private float areaDamageRadius = 5f;
    private int damage = 5; 

    private Transform targetTransform;
    private Vector3 targetDirection;

    private bool targetIsSet = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (targetIsSet)
        {
            MoveToTarget();
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        targetTransform = newTarget.transform;
        targetIsSet = true;
        targetDirection = targetTransform.position - transform.position;
        transform.LookAt(targetTransform.position);
    }

    private void MoveToTarget()
    {
        rb.velocity = targetDirection.normalized * rocketSpeed * Time.fixedDeltaTime;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        Destroy(gameObject);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, areaDamageRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("enemy"))
            {
                hitCollider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}