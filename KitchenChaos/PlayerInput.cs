using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnAltInteractAction;
    private InputSystem_Actions action;
    private InputSystem_Actions.PlayerActions m_player;
    private void Awake()
    {
        action = new InputSystem_Actions(); 
        m_player = action.Player;

        m_player.Interact.performed += Interact_performed;
        m_player.AlternateInteract.performed += AlternateInteract_performed;
    }

    private void AlternateInteract_performed(InputAction.CallbackContext obj)
    {
        OnAltInteractAction(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction(this, EventArgs.Empty);
    }

    public Vector2 PlayerInputNormalised()
    {
        Vector2 inputVector = action.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }

    private void OnEnable()
    {
        m_player.Enable();
    
    }

    private void OnDisable()
    {
        m_player.Disable();
    }

}




//Vector2 inputVector = new Vector2(0, 0);
//if (Input.GetKey(KeyCode.W))
//{
//    inputVector.y = +1;
//}
//if (Input.GetKey(KeyCode.S))
//{
//    inputVector.y = -1;
//}
//if (Input.GetKey(KeyCode.D))
//{
//    inputVector.x = +1;
//}
//if (Input.GetKey(KeyCode.A))
//{
//    inputVector.x = -1;
//}

//inputVector = inputVector.normalized;
