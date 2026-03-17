using UnityEngine;

public class CounterContainer : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectsSO spawnObject;

    private KitchenObjects kitchenObject;

    public override void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Debug.Log("Interaction Performed");
            kitchenObject = KitchenObjects.SpawnKitchenObject(spawnObject, player);

        }

    }

    public KitchenObjects SetKitchenObjectParent()
    {
        throw new System.NotImplementedException();
    }

    public KitchenObjects SpawnKitchenObject()
    {
        throw new System.NotImplementedException();
    }

    public Transform GetObjectFollowTransform()
    {
        return counterTopPosition;
    }
}
