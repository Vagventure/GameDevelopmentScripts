using System;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(PlayerControl player)
    {
        if (player.HasKitchenobject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) 
            {
               DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
               player.GetKitchenObject().DestroySelf(); 
            }
        }
    }
}
