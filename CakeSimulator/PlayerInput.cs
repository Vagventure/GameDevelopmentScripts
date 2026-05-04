using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private const string PLAYER_INPUT_BINDINGS = "PlayerInputBindings";
    public static PlayerInput Instance { get; private set; }
    public event EventHandler OnInteractPerformed;
    public event EventHandler OnAltInteractperformed;
    public event EventHandler OnPauseperformed;

    private InputSystem_Actions action;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        AltIntract,
        Pause,
        Controller_Interact,
        Controller_AltIntract,
        Controller_Pause
    }
    private void Awake()
    {
        Instance = this;
        action = new InputSystem_Actions();

        if (PlayerPrefs.HasKey(PLAYER_INPUT_BINDINGS))
        {
            action.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_INPUT_BINDINGS));
        }

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

    public string GetKeyBinding(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Interact:
                return action.Player.Interact.bindings[0].ToDisplayString();
            case Binding.AltIntract:
                return action.Player.AltInteract.bindings[0].ToDisplayString();
            case Binding.Pause:
                return action.Player.Pause.bindings[0].ToDisplayString();
            case Binding.Controller_Interact:
                return action.Player.Interact.bindings[1].ToDisplayString();
            case Binding.Controller_AltIntract:
                return action.Player.AltInteract.bindings[1].ToDisplayString();
            case Binding.Controller_Pause:
                return action.Player.Pause.bindings[1].ToDisplayString();
            case Binding.Move_Up:
                //LogBindings(action.Player.Move, "Before Rebind");
                return action.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Down:
                return action.Player.Move.bindings[4].ToDisplayString();
            case Binding.Move_Left:
                return action.Player.Move.bindings[6].ToDisplayString();
            case Binding.Move_Right:
                return action.Player.Move.bindings[8].ToDisplayString();
        }
    }

    void LogBindings(InputAction action, string tag)
    {
        Debug.Log($"---- {tag} ----");

        for (int i = 0; i < action.bindings.Count; i++)
        {
            var b = action.bindings[i];

            Debug.Log(
                $"Index: {i} | " +
                $"Name: {b.name} | " +
                $"Path: {b.path} | " +
                $"Override: {b.overridePath} | " +
                $"IsComposite: {b.isComposite} | " +
                $"PartOfComposite: {b.isPartOfComposite} | " +
                $"ID: {b.id}"
            );
        }
    }

    public void SetKeyBinding(Binding binding, Action OnActionRebound)
    {
        OnDisable();
        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            default:
            case Binding.Interact:
                inputAction = action.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.AltIntract:
                inputAction = action.Player.AltInteract;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = action.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.Controller_Interact:
                inputAction = action.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.Controller_AltIntract:
                inputAction = action.Player.AltInteract;
                bindingIndex = 1;
                break;
            case Binding.Controller_Pause:
                inputAction = action.Player.Pause;
                bindingIndex = 1;
                break;
            case Binding.Move_Up:
                inputAction = action.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Down:
                inputAction = action.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Move_Left:
                inputAction = action.Player.Move;
                bindingIndex = 6;
                break;
            case Binding.Move_Right:
                inputAction = action.Player.Move;
                bindingIndex = 8;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            OnEnable();
            OnActionRebound();
            //LogBindings(action.Player.Move, "After Rebind");
            PlayerPrefs.SetString(PLAYER_INPUT_BINDINGS, action.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
        
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
