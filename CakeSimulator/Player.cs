using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    [SerializeField]private PlayerInput input;
    [SerializeField]private float moveSpeed = 7.0f;
    [SerializeField]private float rotationSpeed = 3.0f;
    [SerializeField]private float maxCollisionDistance = 0.2f;
    [SerializeField]private float playerHeight = 2f;
    [SerializeField] private float playerRadius = 0.7f;

    private bool isWalking;

    private void Update()
    {
        HandleMovement();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = input.PlayerInputNormalised();
        Vector3 newMov = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = newMov != Vector3.zero;

        float moveDistance = moveSpeed * Time.deltaTime;
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
}
