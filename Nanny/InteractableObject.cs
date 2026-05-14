using Unity.VisualScripting;
using UnityEngine;

public class InteractableObject : BaseObject
{
    [SerializeField] private InteractableObjectSO interactableObject;
    override public void Interact()
    {
        Debug.Log(GetInteractableObjectSO().name);
    }

    public InteractableObjectSO GetInteractableObjectSO()
    {
        return interactableObject;
    }

    public bool IsInteractableObject()
    {
        return interactableObject != null;
    }
}

