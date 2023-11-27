using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, SphereControls.ISphereActions
{
    public event Action NextWeaponSwitchEvent;
    public event Action PreviousWeaponSwitchEvent;
    public event Action ShootEvent;
    public event Action ReleaseShotEvent;

    public Vector2 MovementValue { get; private set; }
    public Vector2 AimValue { get; private set; }
    public bool StopMovement { get; private set; }
    public bool AutoAim { get; private set; }
    
    public void OnMove(InputAction.CallbackContext context) => MovementValue = context.ReadValue<Vector2>();
    public void OnJoystickAim(InputAction.CallbackContext context) => AimValue = context.ReadValue<Vector2>();
    public void OnNextWeapon(InputAction.CallbackContext context)
    {
        if (context.performed) NextWeaponSwitchEvent?.Invoke();
    }

    public void OnPreviousWeapon(InputAction.CallbackContext context)
    {
        if (context.performed) PreviousWeaponSwitchEvent?.Invoke();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShootEvent?.Invoke();
        }

        if (context.canceled)
        {
            ReleaseShotEvent?.Invoke();
        }
        
    }

    public void OnStop(InputAction.CallbackContext context)
    {
        if (context.performed) StopMovement = true;
        if (context.canceled) StopMovement = false;
    }

    public void OnAutoAim(InputAction.CallbackContext context)
    {
        if (context.started) AutoAim = true;
        if (context.canceled) AutoAim = false;
    }

}
