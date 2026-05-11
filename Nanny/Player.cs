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

    [SerializeField, Tooltip("How fast the player rotates to face the move direction")]
    private float rotationSpeed = 10f;

    [Header("Interaction Settings")]

    [SerializeField, Tooltip("How far in front of the player the interact raycast reaches")]
    private float interactRange = 2f;

    [SerializeField, Tooltip("Which layers count as interactable objects")]
    private LayerMask interactableLayer;

    [Header("Capsule Collision")]
    [SerializeField, Tooltip("Height of the collision capsule — match your character height")]
    private float playerHeight = 2f;

    [SerializeField, Tooltip("Radius of the collision capsule — match your character width")]
    private float playerRadius = 0.5f;
    // ──────────────────────────────────────────────
    // PRIVATE REFERENCES
    // ──────────────────────────────────────────────

    // Reference to the input handler sitting on the same GameObject
    [SerializeField]private PlayerInput _input;

    // Unity's built-in character controller — handles collision and movement
    private CharacterController _controller;

    // Tracks vertical velocity (for gravity)
    private Vector3 _velocity;
    private bool isWalking;
    private bool isSprinting;


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
    //private void HandleMovement()
    //{
    //    // Read the direction input (X = strafe, Y = forward/back)
    //    Vector2 input = _input.MoveInput;

    //    // Build a 3D direction from the 2D input
    //    // We use the player's local forward/right axes so movement
    //    // is always relative to which way the player is facing
    //    Vector3 moveDirection = transform.right * input.x
    //                          + transform.forward * input.y;

    //    // Choose speed based on whether sprint is held
    //    float currentSpeed = _input.IsSprinting ? sprintSpeed : movementSpeed;

    //    // Apply the movement via CharacterController (handles collision automatically)
    //    _controller.Move(moveDirection * currentSpeed * Time.deltaTime);
    //}

    private void HandleMovement()
    {
        Vector2 inputVector = _input.MoveInput.normalized;
        //Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = (camForward * inputVector.y) + (camRight * inputVector.x);


        isWalking = moveDir != Vector3.zero;
        isSprinting = _input.IsSprinting && isWalking;

        float currentSpeed = isSprinting ? sprintSpeed : movementSpeed;
        float moveDistance = currentSpeed * Time.deltaTime;

        // Capsule top and bottom — inset by radius so the shape is accurate
        Vector3 capsuleBottom = transform.position + Vector3.up * playerRadius;
        Vector3 capsuleTop = transform.position + Vector3.up * (playerHeight - playerRadius);

        // Primary cast: try full desired direction
        bool canMove = !Physics.CapsuleCast(
            capsuleBottom, capsuleTop,
            playerRadius, moveDir, moveDistance
        );

        if (!canMove)
        {
            // Slide attempt 1: Z axis only (forward / back)
            Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
            canMove = moveDirZ != Vector3.zero && !Physics.CapsuleCast(
                capsuleBottom, capsuleTop,
                playerRadius, moveDirZ, moveDistance
            );

            if (canMove)
            {
                moveDir = moveDirZ;
            }
            else
            {
                // Slide attempt 2: X axis only (left / right)
                Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
                canMove = moveDirX != Vector3.zero && !Physics.CapsuleCast(
                    capsuleBottom, capsuleTop,
                    playerRadius, moveDirX, moveDistance
                );

                if (canMove)
                {
                    moveDir = moveDirX;
                }
                else
                {
                    Debug.Log("Collided — fully blocked");
                }
            }
        }

        // Feed the resolved direction into CharacterController
        // It applies its own depenetration on top of our cast checks
        if (canMove)
        {
            _controller.Move(moveDir * moveDistance);
        }

        // Rotate smoothly to face movement direction
        if (moveDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(
                transform.forward,
                moveDir,
                rotationSpeed * Time.deltaTime
            );

        }
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

