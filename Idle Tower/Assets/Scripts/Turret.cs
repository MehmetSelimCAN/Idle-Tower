using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform towerFirePoint;
    [SerializeField] private float shootCooldown = 0.5f;
    [SerializeField] private BulletDataSO bulletData;

    private bool isShooting = false;
    private float lastShootTime = 0f;
    private Vector3 targetPosition;
    [SerializeField] private float targetIconMoveSpeed = 10;

    private Camera mainCam;
    
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Vector2 joystickSize = new Vector2(300, 300);
    
    private ETouch.Finger movementFinger;
    private Vector2 movementAmount;

    [SerializeField] private GameObject targetCube;

    private float rotationSpeed = 10f;

    private void OnEnable()
    {
        ETouch.EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HoldFingerUp;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void HandleFingerMove(ETouch.Finger movedFinger)
    {
        if (movedFinger == movementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = joystickSize.x / 2f;
            ETouch.Touch currentTouch = movedFinger.currentTouch;

            if (Vector2.Distance(
                currentTouch.screenPosition,
                joystick.GetComponent<RectTransform>().anchoredPosition
            ) > maxMovement)
            {
                knobPosition = (
                                   currentTouch.screenPosition - joystick.GetComponent<RectTransform>().anchoredPosition
                               ).normalized
                               * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - joystick.GetComponent<RectTransform>().anchoredPosition;
            }

            joystick.knob.anchoredPosition = knobPosition;
            movementAmount = knobPosition / maxMovement;
        }
    }

    private void HoldFingerUp(ETouch.Finger lostFinger)
    {
        if (lostFinger == movementFinger)
        {
            movementFinger = null;
            joystick.knob.anchoredPosition = Vector2.zero;
            joystick.gameObject.SetActive(false);
            movementAmount = Vector2.zero;
        }
    }

    private void HandleFingerDown(ETouch.Finger touchedFinger)
    {
        if (movementFinger == null)
        {
            movementFinger = touchedFinger;
            movementAmount = Vector2.zero;
            joystick.gameObject.SetActive(true);
            joystick.GetComponent<RectTransform>().sizeDelta = joystickSize;
            joystick.GetComponent<RectTransform>().anchoredPosition = ClampStartPosition(touchedFinger.screenPosition);
        }
    }
    
    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        if (startPosition.x < joystickSize.x / 2)
        {
            startPosition.x = joystickSize.x / 2;
        }

        if (startPosition.y < joystickSize.y / 2)
        {
            startPosition.y = joystickSize.y / 2;
        }
        else if (startPosition.y > Screen.height - joystickSize.y / 2)
        {
            startPosition.y = Screen.height - joystickSize.y / 2;
        }

        return startPosition;
    }


   
    
    private void Start()
    {
        mainCam = Camera.main;
        shootCooldown = bulletData.fireRate;
    }
    private void OnDisable()
    {
        ETouch.EnhancedTouchSupport.Disable();
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HoldFingerUp;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
    }
    private void Update()
    {
        HandleShooting();
        RotateTurretHead();
        MoveTargetIcon();
    }

    private void HandleShooting()
    {
        if (ETouch.Touch.activeTouches.Count > 0)
        {
            if (!isShooting)
            {
                StartShooting();
            }
            targetPosition = targetCube.transform.position;
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
    }

    private void StopShooting()
    {
        isShooting = false;
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

    private void MoveTargetIcon()
    {
        Vector3 moveDirection = new Vector3(movementAmount.x, 0, movementAmount.y);
        Vector3 currentPosition = targetCube.transform.position;
        Vector3 newPosition = currentPosition + moveDirection * targetIconMoveSpeed * Time.deltaTime;
        targetCube.transform.position = newPosition;
    }
    
    
    private void RotateTurretHead()
    {
        Vector3 targetPosition = targetCube.transform.position;

        // Calculate the direction from the turret's position to the target position
        Vector3 directionToTarget = targetPosition - transform.position;
        directionToTarget.y = 0f; // Set the Y-component to 0 to restrict rotation to the Y-axis

        // Use LookAt to make the turret instantly face the target in the Y-axis
        transform.LookAt(targetPosition);

        // Reset the turret's rotation on the X and Z axes to maintain its original orientation
        Quaternion originalRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        transform.rotation = originalRotation;
    }


    
}
