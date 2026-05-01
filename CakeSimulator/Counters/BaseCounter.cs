using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] public Transform counterTopPosition;

    private KitchenObjects kitchenObject;
    public virtual void Interact(Player player){}
    public virtual void AltInteract(Player player) {}


    public void SetKitchenObjectParent(KitchenObjects kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObjects SpawnKitchenObject()
    {
        throw new System.NotImplementedException();
    }

    public Transform GetObjectFollowTransform()
    {
        return counterTopPosition;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public KitchenObjects GetKitchenObjects()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObjects kitchenObj)
    {
        this.kitchenObject = kitchenObj;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

}
