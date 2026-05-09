using UnityEngine;

// This script drives the Animator based on player movement state.
// It reads from PlayerInputHandler and sets the boolean parameters
// you defined: IsWalking, IsRunning, IsCrouching.
// Attach this to your Player GameObject alongside the other scripts.
//[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerAnimationController : MonoBehaviour
{
    // ──────────────────────────────────────────────
    // SERIALIZED FIELDS
    // ──────────────────────────────────────────────

    [Header("Smoothing")]
    [SerializeField, Tooltip("How quickly the animation blends in and out (lower = smoother)")]
    private float transitionSpeed = 10f;

    [Header("Crouch")]
    [SerializeField, Tooltip("Hold this key to crouch (default: Left Ctrl)")]
    private KeyCode crouchKey = KeyCode.LeftControl;


    // ──────────────────────────────────────────────
    // ANIMATOR PARAMETER HASHES
    // We cache these as ints instead of passing strings every frame.
    // Hashes are faster — Unity looks them up by ID, not by name.
    // ──────────────────────────────────────────────
    private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
    private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");
    private static readonly int IsCroucheHash = Animator.StringToHash("IsCrouched"); // matches your exact parameter name


    // ──────────────────────────────────────────────
    // PRIVATE REFERENCES
    // ──────────────────────────────────────────────
    private Animator _animator;
    private PlayerInput _input;

    // Smoothed speed value used to decide which state we're in
    private float _smoothSpeed;


    // ──────────────────────────────────────────────
    // Awake: grab references before anything else runs
    // ──────────────────────────────────────────────
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<PlayerInput>();
    }


    // ──────────────────────────────────────────────
    // Update: evaluate movement state every frame
    // and push the right booleans to the Animator
    // ──────────────────────────────────────────────
    private void Update()
    {
        HandleLocomotionAnimation();
        HandleCrouchAnimation();
    }


    // ──────────────────────────────────────────────
    // LOCOMOTION
    // Smoothly interpolates a speed value, then uses it
    // to decide between Idle → Walking → Running.
    // ──────────────────────────────────────────────
    private void HandleLocomotionAnimation()
    {
        // The raw input magnitude: 0 = no input, 1 = moving
        float inputMagnitude = _input.MoveInput.magnitude;

        // Smoothly move _smoothSpeed toward the target magnitude
        // This prevents snapping instantly between states
        _smoothSpeed = Mathf.Lerp(_smoothSpeed, inputMagnitude, Time.deltaTime * transitionSpeed);

        // Decide states based on smoothed speed and sprint flag
        bool isMoving = _smoothSpeed > 0.1f;              // above dead-zone = moving
        bool isSprinting = _input.IsSprinting && isMoving;  // sprinting only counts while moving

        // Push booleans to the Animator
        // IsWalking: true when moving but NOT sprinting
        _animator.SetBool(IsWalkingHash, isMoving && !isSprinting);

        // IsRunning: true when moving AND sprinting
        _animator.SetBool(IsRunningHash, isSprinting);
    }


    // ──────────────────────────────────────────────
    // CROUCH
    // Toggles the IsCrouche bool when the crouch key is held.
    // Crouching is cancelled automatically if the player sprints.
    // ──────────────────────────────────────────────
    private void HandleCrouchAnimation()
    {
        bool wantsToCrouch = _input.IsCrouched;

        // Can't crouch and sprint at the same time
        bool isCrouching = wantsToCrouch && !_input.IsSprinting;

        _animator.SetBool(IsCroucheHash, isCrouching);
    }
}