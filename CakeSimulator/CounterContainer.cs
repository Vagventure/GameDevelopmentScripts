using System;
using UnityEngine;

public class CounterContainer : BaseCounter, IKitchenObjectParent
{
    public event EventHandler OnInteractionPerformed;
    [SerializeField] private KitchenObjectsSO spawnObject;

    
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            Debug.Log("Interaction Performed");
            KitchenObjects spawnObj =  KitchenObjects.SpawnKitchenObject(spawnObject, player);
            player.SetKitchenObject(spawnObj);

            OnInteractionPerformed?.Invoke(this,EventArgs.Empty);

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
        throw new System.NotImplementedException();
    }

    public KitchenObjects GetKitchenObjects()
    {
        throw new System.NotImplementedException();
    }

    public void SetKitchenObject(KitchenObjects kitchenObject)
    {
        throw new System.NotImplementedException();
    }

    public void ClearKitchenObject()
    {
        throw new System.NotImplementedException();
    }
}
