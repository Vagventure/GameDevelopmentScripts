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
    [SerializeField] private List<KitchenObjectsSO> validIcingObjectSOList;

    private KitchenObjectsSO bakedCakeBase;
    private KitchenObjectsSO unBakedCakeBase;
    private List<KitchenObjectsSO> kitchenPlateObjectsList;
    private bool doesNotContainsIcing = true;

    private void Awake()
    {
        kitchenPlateObjectsList = new List<KitchenObjectsSO>();
        bakedCakeBase = validKitchenPlateObjectSOList[3];
        unBakedCakeBase = validKitchenPlateObjectSOList[4];
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

        if (validIcingObjectSOList.Contains(kitchenObjectsSO))
        {
            if (doesNotContainsIcing)
            {
                kitchenPlateObjectsList.Add(kitchenObjectsSO);
                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
                {
                    kitchenObjectsSO = kitchenObjectsSO
                });
                doesNotContainsIcing = false;
                return true;
            }

            return false;
           
        }
        //if (kitchenObjectsSO == validKitchenPlateObjectSOList[3])
        //{
        //    foreach (KitchenObjectsSO kitchenObject in kitchenPlateObjectsList)
        //    {
        //        //Finding match for unBakedBase
        //        if (kitchenObject == validKitchenPlateObjectSOList[4])
        //        {
        //            kitchenPlateObjectsList.Remove(kitchenObject);
        //        }
        //    }
        //}
        if (kitchenObjectsSO == bakedCakeBase)
        {
            foreach (KitchenObjectsSO kitchenObject in kitchenPlateObjectsList)
            {
                //Finding match for unBakedBase
                if (kitchenObject == unBakedCakeBase)
                {
                    kitchenPlateObjectsList.Remove(kitchenObject);
                }
            }
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

    public List<KitchenObjectsSO> GetKitchenObjectsSOList()
    {
        return kitchenPlateObjectsList;
    }
}
