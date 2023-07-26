using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private Vector3 desiredPosition;
    private Vector3 smoothPosition;
    private float smoothSpeed = 0.125f;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    private void FixedUpdate()
    {
        desiredPosition = target.position + offset;
        smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;
    }
}
