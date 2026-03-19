using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField]private PlayerInput input;
    [SerializeField]private float moveSpeed = 7.0f;
    [SerializeField]private float rotationSpeed = 3.0f;
    [SerializeField]private float maxCollisionDistance = 0.2f;
    [SerializeField]private float playerHeight = 2f;
    [SerializeField]private float playerRadius = 0.7f;

    private bool isWalking;
    private Vector3 lastInteraction;
    private BaseCounter selectedCounter;
    private KitchenObjects kitchenObject;
  

    [SerializeField] private Transform objectHoldPoint;

    private void Start()
    {
        input.OnInteractPerformed += Input_OnInteractPerformed;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More than one instance of player exists");
        }
        Instance = this;
    }

    private void Input_OnInteractPerformed(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
           selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = input.PlayerInputNormalised();
        Vector3  newMov = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = newMov != Vector3.zero;

        float moveDistance = Mathf.Min(moveSpeed * Time.deltaTime);
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius, newMov ,moveDistance);
     
        if (!canMove)
        {
            Vector3 movDirX = new Vector3(0, 0, newMov.z).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDirX, moveDistance);
            if (canMove)
            {
                newMov = movDirX;
            }
            else
            {
                Vector3 movDirY = new Vector3(0, 0, newMov.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDirY, moveDistance);
                if (canMove)
                {
                    newMov = movDirY;
                }

            }
            Debug.Log("Collided");

        }
        if (canMove)
        {
            transform.position += newMov * moveDistance;
        }
        transform.forward = Vector3.Slerp(transform.forward, newMov, rotationSpeed * Time.deltaTime);
              
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = input.PlayerInputNormalised();
        Vector3 newMov = new Vector3(inputVector.x, 0f, inputVector.y);

        float maxInteractDistance = 5f;

        if(newMov != Vector3.zero)
        {
            lastInteraction = newMov;
        }

        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit hitInfo, maxInteractDistance))
        {
            if(hitInfo.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //Debug.Log("hit");
                if (baseCounter != selectedCounter)
                {
                    selectedCounter = baseCounter;

                    SetSelectedCounter(selectedCounter);
                //Debug.Log("Invoked");

                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
            //Debug.Log("Nothing hit");
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (!Application.isPlaying) return;

    //    Gizmos.color = Color.red;

    //    float moveDistance = moveSpeed * Time.deltaTime;

    //    Vector3 point1 = transform.position + Vector3.up * playerRadius;
    //    Vector3 point2 = transform.position + Vector3.up * (playerHeight - playerRadius);
    //    Vector3 direction = newMov.normalized;

    //    // Start capsule
    //    Gizmos.DrawWireSphere(point1, playerRadius);
    //    Gizmos.DrawWireSphere(point2, playerRadius);

    //    // End capsule
    //    Vector3 end1 = point1 + direction * moveDistance;
    //    Vector3 end2 = point2 + direction * moveDistance;

    //    Gizmos.DrawWireSphere(end1, playerRadius);
    //    Gizmos.DrawWireSphere(end2, playerRadius);

    //    // Lines
    //    Gizmos.DrawLine(point1, end1);
    //    Gizmos.DrawLine(point2, end2);
    //}
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetObjectFollowTransform()
    {
        return objectHoldPoint;
    }

    public KitchenObjects SpawnKitchenObject()
    {
        throw new NotImplementedException();
    }

    public KitchenObjects SetKitchenObjectParent()
    {
        throw new NotImplementedException();
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
     
    }

    public KitchenObjects GetKitchenObjects()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObjects kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
}
