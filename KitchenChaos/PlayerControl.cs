using System;
using UnityEngine;

public class PlayerControl : BaseCounter, IKitchenObjectParent
{
    public static PlayerControl Instance { get; private set; }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private PlayerInput input;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private LayerMask clearCounterMask;
    [SerializeField] private Transform playerObjHoldPoint;


    public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedArgs: EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    private float playerRadius = 0.7f;
    private BaseCounter selectedCounter;
    private Vector3 lastInteraction = Vector3.zero;
    private KitchenObjects kitchenObj;

    private bool isWalking;


    private void Awake()
    {
        if (Instance != null) {
            Debug.Log("More than one instance of player exists");
        }   
        Instance = this;
    }

    private void Start()
    {
        input.OnInteractAction += Input_OnInteractAction;
        input.OnAltInteractAction += Input_OnAltInteractAction;
    }

    private void Input_OnAltInteractAction(object sender, EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.AltInteract(this);
        }
    }

    private void Input_OnInteractAction(object sender, System.EventArgs e)
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

    public void HandleMovement()
    {
        Vector2 inputVector = input.PlayerInputNormalised();
        Vector3 newMov = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = newMov != Vector3.zero;

        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, newMov, moveDistance);

        if (!canMove)
        {
            //Debug.Log("Collided");

            Vector3 moveDirX = new Vector3(0, 0, newMov.z).normalized;
            canMove = moveDirX.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                newMov = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, newMov.z).normalized;
                canMove = moveDirZ.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    newMov = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += newMov * moveDistance;
        }
        transform.forward = Vector3.Slerp(transform.forward, newMov, Time.deltaTime * rotationSpeed);

    }

    private void HandleInteraction()
    {
        Vector2 inputVector = input.PlayerInputNormalised();
        Vector3 newMov = new Vector3(inputVector.x, 0f, inputVector.y);
       
        float interactDistance = 2f;

        if(newMov != Vector3.zero)
        {
            lastInteraction = newMov;
        }

        
        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit hitInfo, interactDistance, clearCounterMask))
        {
            if (hitInfo.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //clearCounter.Interact();
                if(baseCounter != selectedCounter)
                {
                    selectedCounter = baseCounter;

                    SetSelectedCounter(selectedCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        } else
        {
            SetSelectedCounter(null);
        }

        //Debug.Log(selectedCounter);

    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedArgs
        {
            selectedCounter = selectedCounter
        });
    }

}

