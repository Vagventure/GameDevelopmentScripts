using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    // ──────────────────────────────────────────────
    // Reference to the generated Input Actions class
    // (Unity auto-generates this from your .inputactions asset)
    // ──────────────────────────────────────────────
    private InputSystem_Actions _controls; // Replace "PlayerControls" with your generated class name

    // ──────────────────────────────────────────────
    // Stored input values — read these from PlayerController
    // ──────────────────────────────────────────────
    public Vector2 MoveInput { get; private set; } // WASD / Left Stick direction
    public bool IsSprinting { get; private set; } // True while Sprint is held
    public bool InteractPressed { get; private set; } // True for one frame on press
    public bool IsCrouched { get; private set; }

    // ──────────────────────────────────────────────
    // OnEnable: called when the GameObject becomes active.
    // We create controls AND enable them here in one place.
    // This avoids Awake() execution-order issues where another
    // script's Awake() tries to read input before _controls exists.
    // ──────────────────────────────────────────────
    private void OnEnable()
    {
        // Create the controls instance if it doesn't exist yet
        if (_controls == null)
        {
            _controls = new InputSystem_Actions();

            // ── Move ──────────────────────────────────
            _controls.Player.Move.performed += OnMove;
            _controls.Player.Move.canceled += OnMoveCanceled;

            // ── Sprint ────────────────────────────────
            _controls.Player.Sprint.performed += OnSprintStarted;
            _controls.Player.Sprint.canceled += OnSprintCanceled;

            // ── Crouch ────────────────────────────────
            _controls.Player.Crouch.performed += OnCrouchStarted;
            _controls.Player.Crouch.canceled += OnCrouchCanceled;

            // ── Interact ──────────────────────────────
            _controls.Player.Interact.performed += OnInteract;
        }

        // Enable the Player action map so input starts being read
        _controls.Player.Enable();
    }


    // ──────────────────────────────────────────────
    // OnDisable: called when the GameObject is deactivated.
    // Always disable controls to stop listening and avoid errors.
    // ──────────────────────────────────────────────
    private void OnDisable()
    {
        // Guard in case OnDisable fires before OnEnable ever ran
        if (_controls == null) return;

        _controls.Player.Disable();
    }


    // ──────────────────────────────────────────────
    // Update: reset single-frame flags so they don't
    // stay true longer than one frame.
    // ──────────────────────────────────────────────
    private void Update()
    {
        InteractPressed = false;
    }


    // ──────────────────────────────────────────────
    // INPUT CALLBACKS
    // Called automatically by the Input System.
    // ──────────────────────────────────────────────

    // Called while Move input has a non-zero value
    private void OnMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    // Called when Move input returns to zero
    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        MoveInput = Vector2.zero;
    }

    // Called when Sprint is pressed / held
    private void OnSprintStarted(InputAction.CallbackContext ctx)
    {
        IsSprinting = true;
    }

    // Called when Sprint is released
    private void OnSprintCanceled(InputAction.CallbackContext ctx)
    {
        IsSprinting = false;
    }

    private void OnCrouchStarted(InputAction.CallbackContext ctx)
    {
        IsCrouched = true;
    }

    // Called when Sprint is released
    private void OnCrouchCanceled(InputAction.CallbackContext ctx)
    {
        IsCrouched = false;
    }

    // Called once when Interact is pressed
    private void OnInteract(InputAction.CallbackContext ctx)
    {
        InteractPressed = true;
    }
}
