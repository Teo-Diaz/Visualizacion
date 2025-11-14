using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using static UnityEngine.InputSystem.InputSystem_Actions;

[CreateAssetMenu(fileName = "New Input Provider", menuName = "InputProvider")]
public class InputProvider : ScriptableObject, IPlayerActions
{
    private InputSystem_Actions _basicControls;

    private void OnEnable()
    {
        if (_basicControls != null) return;

        _basicControls = new InputSystem_Actions();

        ReleaseStandard();

        _basicControls.Player.SetCallbacks(this);

        SetStandard();
    }

    private void OnDisable()
    {
        ReleaseStandard();
    }

    public void SetStandard()
    {
        _basicControls.Player.Enable();
    }

    public void ReleaseStandard() => _basicControls.Player.Disable();

    public event Action OnFirePerformed;
    public event Action OnFireCanceled;

    public void OnAim(CallbackContext context)
    {
    }

    public void OnCancel(CallbackContext context)
    {
    }

    public void OnFire(CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Performed:
                OnFirePerformed?.Invoke();
                break;
            case InputActionPhase.Canceled:
                OnFireCanceled?.Invoke();
                break;
        }
    }

    public void OnFire2(CallbackContext context)
    {
    }

    public void OnJump(CallbackContext context)
    {
    }

    public void OnLook(CallbackContext context)
    {
    }

    public void OnMove(CallbackContext context)
    {
    }

    public void OnSprint(CallbackContext context)
    {
    }

    public void OnZoom(CallbackContext context)
    {
    }
}