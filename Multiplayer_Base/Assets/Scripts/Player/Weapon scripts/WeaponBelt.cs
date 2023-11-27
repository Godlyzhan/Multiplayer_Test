using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the rotation of the belt surrounding the player
/// </summary>
public class WeaponBelt : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private float smoothTime = 0.05f;
    [SerializeField] private float turnSpeed = 1f;
    [SerializeField] private Transform RayCastPoint;
    
    public List<Target> targets = new List<Target>();
    
    private float currentVelocity;
    private bool lockOnTarget;
    private Vector3 targetPosition;

    private void Update()
    {
        transform.position = playerPosition.position;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        Debug.DrawRay(RayCastPoint.position, transform.forward * 100f, Color.blue);
        if (Physics.Raycast(RayCastPoint.position, transform.forward, out hit))
        {
            if (inputHandler.AutoAim && hit.transform.TryGetComponent(out Target target))
            {
                targetPosition = hit.transform.position;
                lockOnTarget = true;
            }
            else if(inputHandler.AutoAim && targets.Count > 0)
            {
                targetPosition = targets[0].transform.position;
                lockOnTarget = true;
            }
            else
            {
                lockOnTarget = false;
            }
        }

        RotateBelt();
    }

    private void RotateBelt()
    {
        if (lockOnTarget)
        {
            Quaternion _lookRotation = Quaternion.LookRotation((targetPosition - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
        }
        else
        {
            if (Mathf.Approximately(inputHandler.AimValue.sqrMagnitude, 0f)) return;

            float targetRotation = Mathf.Atan2(inputHandler.AimValue.x, inputHandler.AimValue.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Target target))
        {
            targets.Add(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Target target))
        {
            targets.Remove(target);
        }
    }
}
