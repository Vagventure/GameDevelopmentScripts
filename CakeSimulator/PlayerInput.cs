using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    public event EventHandler OnInteractPerformed;
    public event EventHandler OnAltInteractperformed;
    public event EventHandler OnPauseperformed;

    private InputSystem_Actions action;
    private void Awake()
    {
        Instance = this;
        action = new InputSystem_Actions();

        action.Player.Interact.performed += Interact_performed;
        action.Player.AltInteract.performed += AltInteract_performed;
        action.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseperformed?.Invoke(this,EventArgs.Empty); 
    }

    private void AltInteract_performed(InputAction.CallbackContext obj)
    {
        OnAltInteractperformed?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractPerformed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 PlayerInputNormalised()
    {
        Vector2 inputVector = action.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        
        return inputVector;
    }

    private void OnEnable()
    {
        action.Player.Enable();
    }

    private void OnDisable()
    {
        action.Player.Disable();
    }
}
