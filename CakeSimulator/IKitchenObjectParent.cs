using UnityEngine;

public interface IKitchenObjectParent
{
    public KitchenObjects SpawnKitchenObject();

    public KitchenObjects SetKitchenObjectParent();

    public Transform GetObjectFollowTransform();
    
}
