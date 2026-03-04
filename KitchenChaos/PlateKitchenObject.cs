using System.Collections.Generic;
using UnityEngine;


public class PlateKitchenObject : KitchenObjects
{
    [SerializeField]private List<KitchenObjectsSO> validKitchenObjectSOList;
    private List<KitchenObjectsSO> kitchenObjectsSOList;

    private void Awake()
    {
        kitchenObjectsSOList = new List<KitchenObjectsSO>();
    }
    public bool TryAddIngridient(KitchenObjectsSO kitchenObjectsSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectsSO)){
            return false;
        }

        if (kitchenObjectsSOList.Contains(kitchenObjectsSO))
        {
            return false;
        }
        else
        {
            kitchenObjectsSOList.Add(kitchenObjectsSO);
            return true;
        }
        
    }
}
