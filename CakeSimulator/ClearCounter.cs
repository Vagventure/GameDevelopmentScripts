using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                //Don't do anything
                if(player.GetKitchenObjects().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngridient(GetKitchenObjects().GetKitchenObjectsSO()))
                    {
                       GetKitchenObjects().DestroySelf();
                    }
                }
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


}
