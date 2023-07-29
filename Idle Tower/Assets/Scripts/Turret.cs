using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class TowerShooting : MonoBehaviour
{
    public Transform towerFirePoint;
    public float yLevel = 0f;
    public LineRenderer lineRenderer;
    public float shootCooldown = 0.5f;

    private bool isShooting = false;
    private float lastShootTime = 0f;
    private Vector3 targetPosition;

    private Camera mainCam;

    private void OnEnable()
    {
        ETouch.EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        ETouch.EnhancedTouchSupport.Disable();
    }

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        HandleShooting();
        RotateTurretHead();
    }

    private void HandleShooting()
    {
        if (ETouch.Touch.activeTouches.Count > 0)
        {
            if (!isShooting)
            {
                StartShooting();
            }

            targetPosition = GetTargetPositionFromTouch();
            UpdateLineRenderer();

            if (CanShoot())
            {
                FireBullet();
                lastShootTime = Time.time;
            }
        }
        else
        {
            StopShooting();
        }
    }

    private void StartShooting()
    {
        isShooting = true;
        lineRenderer.enabled = true;
    }

    private void StopShooting()
    {
        isShooting = false;
        lineRenderer.enabled = false;
    }

    private bool CanShoot()
    {
        return isShooting && Time.time >= lastShootTime + shootCooldown;
    }

    private void FireBullet()
    {
        GameObject bullet = ObjectPool.Instance.GetPooledObject(0); 
        if (bullet != null)
        {
            bullet.transform.position = towerFirePoint.position;
            bullet.transform.LookAt(targetPosition);

            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = bullet.transform.forward * bullet.GetComponent<Bullet>().speed;
            }

            bullet.SetActive(true);
        }
    }

    private void UpdateLineRenderer()
    {
        lineRenderer.SetPosition(0, towerFirePoint.position);
        lineRenderer.SetPosition(1, targetPosition);
    }

    private Vector3 GetTargetPositionFromTouch()
    {
        if (ETouch.Touch.activeTouches.Count > 0)
        {
            Vector2 touchScreenPos = ETouch.Touch.activeTouches[0].screenPosition;
            Ray ray = mainCam.ScreenPointToRay(touchScreenPos);
            RaycastHit hit;

            // Check if the ray intersects any objects in the scene
            if (Physics.Raycast(ray, out hit))
            {
                // If there's an intersection, return the point of intersection
                return hit.point;
            }

            // If there's no intersection, return the touch position projected to a point in front of the turret
            return ray.GetPoint(100f);
        }
        else
        {
            // If no touches, return a fallback position (e.g., turret's forward direction)
            return transform.position + targetPosition * 100f;
        }
    }

    private void RotateTurretHead()
    {
       transform.LookAt(GetTargetPositionFromTouch());
    }
}
