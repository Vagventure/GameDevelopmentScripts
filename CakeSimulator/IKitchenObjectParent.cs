using UnityEngine;

public interface IKitchenObjectParent
{
    public KitchenObjects SpawnKitchenObject();

    public void SetKitchenObjectParent(KitchenObjects kitchenObject);

    public Transform GetObjectFollowTransform();

    public bool HasKitchenObject();

    public KitchenObjects GetKitchenObjects();

    public void SetKitchenObject(KitchenObjects kitchenObject);

    public void ClearKitchenObject();
    
}
