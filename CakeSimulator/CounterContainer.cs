using System;
using UnityEngine;

public class CounterContainer : BaseCounter
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

}
