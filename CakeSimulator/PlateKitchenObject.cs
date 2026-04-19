using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObjects
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectsSO kitchenObjectsSO;
    }

    [SerializeField] private List<KitchenObjectsSO> validKitchenPlateObjectSOList;
    private List<KitchenObjectsSO> kitchenPlateObjectsList;

    private void Awake()
    {
        kitchenPlateObjectsList = new List<KitchenObjectsSO>();
    }
    public bool TryAddIngridient(KitchenObjectsSO kitchenObjectsSO)
    {
        if(!validKitchenPlateObjectSOList.Contains(kitchenObjectsSO))
        {
            return false;
        }

        if(kitchenPlateObjectsList.Contains(kitchenObjectsSO))
        {
            return false;
        }

        kitchenPlateObjectsList.Add(kitchenObjectsSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
        {
            kitchenObjectsSO = kitchenObjectsSO
        });
        return true;
    }

    public int GetIngredientCount()
    {
        return kitchenPlateObjectsList.Count;
    }
}
