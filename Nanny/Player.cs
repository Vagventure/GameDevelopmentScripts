using UnityEngine;

public class Player : MonoBehaviour
{
    // ──────────────────────────────────────────────
    // SERIALIZED FIELDS
    // These show up in the Unity Inspector so you can
    // tweak values without touching code.
    // ──────────────────────────────────────────────

    [Header("Movement Settings")]

    [SerializeField, Tooltip("How fast the player walks normally")]
    private float movementSpeed = 5f;

    [SerializeField, Tooltip("How fast the player moves while sprinting")]
    private float sprintSpeed = 10f;

    [SerializeField, Tooltip("How strong gravity pulls the player down")]
    private float gravity = -9.81f;

    [Header("Interaction Settings")]

    [SerializeField, Tooltip("How far in front of the player the interact raycast reaches")]
    private float interactRange = 2f;

    [SerializeField, Tooltip("Which layers count as interactable objects")]
    private LayerMask interactableLayer;


    // ──────────────────────────────────────────────
    // PRIVATE REFERENCES
    // ──────────────────────────────────────────────

    // Reference to the input handler sitting on the same GameObject
    [SerializeField]private PlayerInput _input;

    // Unity's built-in character controller — handles collision and movement
    private CharacterController _controller;

    // Tracks vertical velocity (for gravity)
    private Vector3 _velocity;


    // ──────────────────────────────────────────────
    // Awake: grab component references early, before Start runs
    // ──────────────────────────────────────────────
    private void Awake()
    {
        //_input = GetComponent<PlayerInput>();
        _controller = GetComponent<CharacterController>();
    }


    // ──────────────────────────────────────────────
    // Update: runs every frame — handle movement and input checks here
    // ──────────────────────────────────────────────
    private void Update()
    {
        HandleMovement();
        HandleGravity();
        HandleInteract();
    }


    // ──────────────────────────────────────────────
    // MOVEMENT
    // Reads the 2D move input and translates it into
    // 3D world-space movement relative to where the player is facing.
    // ──────────────────────────────────────────────
    private void HandleMovement()
    {
        // Read the direction input (X = strafe, Y = forward/back)
        Vector2 input = _input.MoveInput;

        // Build a 3D direction from the 2D input
        // We use the player's local forward/right axes so movement
        // is always relative to which way the player is facing
        Vector3 moveDirection = transform.right * input.x
                              + transform.forward * input.y;

        // Choose speed based on whether sprint is held
        float currentSpeed = _input.IsSprinting ? sprintSpeed : movementSpeed;

        // Apply the movement via CharacterController (handles collision automatically)
        _controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    }


    // ──────────────────────────────────────────────
    // GRAVITY
    // CharacterController doesn't apply physics automatically,
    // so we add gravity ourselves every frame.
    // ──────────────────────────────────────────────
    private void HandleGravity()
    {
        // If the player is standing on the ground, reset downward velocity
        // A small constant (-2f) keeps the controller grounded reliably
        if (_controller.isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }

        // Accumulate gravity over time (v = a * t)
        _velocity.y += gravity * Time.deltaTime;

        // Apply the vertical velocity to the controller
        _controller.Move(_velocity * Time.deltaTime);
    }


    // ──────────────────────────────────────────────
    // INTERACT CHECK
    // Every frame, if the interact button was just pressed,
    // fire a raycast and call Interact() on whatever was hit.
    // ──────────────────────────────────────────────
    private void HandleInteract()
    {
        // InteractPressed is only true for one frame (reset in PlayerInputHandler.Update)
        if (!_input.InteractPressed) return;

        // Fire a ray from the center of the camera forward (or player forward if no camera)
        Ray ray = new Ray(transform.position + Vector3.up * 1f, transform.forward);

        // Check if the ray hits something on the interactable layer within range
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
        {
            // Try to get an IInteractable from whatever was hit
            // and call its Interact method
            Interact(hit.collider.gameObject);
        }
        else
        {
            Debug.Log("Nothing to interact with."); // Remove once logic is hooked up
        }
    }


    // ──────────────────────────────────────────────
    // INTERACT — open function
    // Called when the player presses Interact and hits something.
    // Hook up your door, NPC, item, or puzzle logic here.
    // ──────────────────────────────────────────────
    public void Interact(GameObject target)
    {
        Debug.Log($"Interacting with: {target.name}");

        // Example: if the target has an IInteractable interface, call it
        // IInteractable interactable = target.GetComponent<IInteractable>();
        // if (interactable != null) interactable.OnInteract();

        // ── Add your interaction logic below ──────


    }


    // ──────────────────────────────────────────────
    // GIZMOS — visualize the interact ray in the Scene view
    // Only visible in the editor, not in builds.
    // ──────────────────────────────────────────────
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 origin = transform.position + Vector3.up * 1f;
        Gizmos.DrawRay(origin, transform.forward * interactRange);
    }
}

