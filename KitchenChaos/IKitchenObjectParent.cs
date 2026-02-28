using UnityEngine;

public interface IKitchenObjectParent
{
    public void SetkitchenObject(KitchenObjects kitchenObj);

    public KitchenObjects GetKitchenObject();
    public void ClearKitchenObject();

    public bool HasKitchenobject();

    public Transform GetKitchenObjectFollowTransform();
}
