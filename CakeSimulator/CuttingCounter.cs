using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectsSO spawnObject;

    private KitchenObjects kitchenObject;

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                //Don't do anything
            }
            else
            {
                //Give object to player
                GetKitchenObjects().SetKitchenObjectParent(player);

            }

        }
        else
        {
            if (player.HasKitchenObject())
            {
                //Place object on the counter
                player.GetKitchenObjects().SetKitchenObjectParent(this);
            }
            else
            {
                //Do nothing
            }

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
