using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private Transform TopOfCounter;

    private KitchenObjects kitchenObj;

    public virtual void Interact(PlayerControl player)
    {
        Debug.LogError("BaseCounter Interact"); 
    }

    public virtual void AltInteract(PlayerControl player)
    {
        Debug.LogError("BaseCounter Alternate_Interact");
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return TopOfCounter;
    }


    public void SetkitchenObject(KitchenObjects kitchenObj)
    {
        this.kitchenObj = kitchenObj;
    }
    public KitchenObjects GetKitchenObject()
    {
        return kitchenObj;
    }

    public void ClearKitchenObject()
    {
        kitchenObj = null;
    }

    public bool HasKitchenobject()
    {
        return kitchenObj != null;
    }
}

