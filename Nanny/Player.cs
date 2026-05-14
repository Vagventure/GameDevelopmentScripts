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

    [Header("Camera")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform cameraPivot;   // drag the camera pivot child here
    [SerializeField] private float verticalClamp = 80f; // max up/down angle
    
    [SerializeField] private PlayerInput _input;


    // ──────────────────────────────────────────────
    // PRIVATE REFERENCES
    // ──────────────────────────────────────────────

    // Reference to the input handler sitting on the same GameObject

    // Unity's built-in character controller — handles collision and movement
    private CharacterController _controller;
    private float _verticalAngle;

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

    private void HandleMovement()
    {
        Vector2 inputVector = _input.MoveInput.normalized;
        Vector2 lookInput = _input.LookInput;

        // ── Camera Look ───────────────────────────────
        // Horizontal mouse rotates the whole player body — this IS the facing direction
        transform.Rotate(Vector3.up * lookInput.x * mouseSensitivity * Time.deltaTime);

        // Vertical mouse tilts only the camera pivot, clamped so it can't flip over
        _verticalAngle -= lookInput.y * mouseSensitivity * Time.deltaTime;
        _verticalAngle = Mathf.Clamp(_verticalAngle, -verticalClamp, verticalClamp);
        cameraPivot.localRotation = Quaternion.Euler(_verticalAngle, 0f, 0f);

        // ── Movement ──────────────────────────────────
        // Build move direction relative to the camera so WASD always feels correct
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

        Vector3 capsuleBottom = transform.position + Vector3.up * playerRadius;
        Vector3 capsuleTop = transform.position + Vector3.up * (playerHeight - playerRadius);

        bool canMove = !Physics.CapsuleCast(capsuleBottom, capsuleTop, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
            canMove = moveDirZ != Vector3.zero && !Physics.CapsuleCast(capsuleBottom, capsuleTop, playerRadius, moveDirZ, moveDistance);

            if (canMove)
            {
                moveDir = moveDirZ;
            }
            else
            {
                Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
                canMove = moveDirX != Vector3.zero && !Physics.CapsuleCast(capsuleBottom, capsuleTop, playerRadius, moveDirX, moveDistance);

                if (canMove) moveDir = moveDirX;
                else Debug.Log("Collided — fully blocked");
            }
        }

        if (canMove)
        {
            _controller.Move(moveDir * moveDistance);
        }

        // REMOVED: Vector3.Slerp on transform.forward — the mouse rotation above
        // already controls facing. Having both caused the snapping you were seeing.
    }

    // ──────────────────────────────────────────────
    // GRAVITY
    // CharacterController doesn't apply physics automatically,
    // so we add gravity ourselves every frame.
    // ──────────────────────────────────────────────
    private void HandleGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }

        _velocity.y += gravity * Time.deltaTime;

        // Pass ONLY vertical velocity — horizontal is handled in HandleMovement()
        _controller.Move(new Vector3(0f, _velocity.y, 0f) * Time.deltaTime);
    }


    // ──────────────────────────────────────────────
    // INTERACT CHECK
    // Every frame, if the interact button was just pressed,
    // fire a raycast and call Interact() on whatever was hit.
    // ──────────────────────────────────────────────
    private void HandleInteract()
    {
        if (!_input.InteractPressed) return;

        // Reset immediately after reading so it only fires once
        _input.ResetInteract();

        Ray ray = new Ray(cameraPivot.position, cameraPivot.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red, 2f);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out InteractableObject interactable))
            {
                interactable.Interact();
            }
            else
            {
                Debug.Log($"Hit '{hit.collider.gameObject.name}' but it has no InteractableObject component.");
            }
        }
        else
        {
            Debug.Log($"Ray hit nothing. Origin: {ray.origin}, Direction: {ray.direction}, Range: {interactRange}");
        }
    }

    // ──────────────────────────────────────────────
    // INTERACT — open function
    // Called when the player presses Interact and hits something.
    // Hook up your door, NPC, item, or puzzle logic here.
    // ──────────────────────────────────────────────
    public void Interact(InteractableObject target)
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

