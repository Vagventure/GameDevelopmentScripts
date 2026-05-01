using UnityEngine;

public class CreamCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO icingSO;
    public override void Interact(Player player)
    {

        if (player.HasKitchenObject())
        {
           
            //Add icing to the base
            if (player.GetKitchenObjects().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                plateKitchenObject.TryAddIngridient(icingSO);  
            }

        }
        
    }
}

