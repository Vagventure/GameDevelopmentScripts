using UnityEngine;
using System;


public class ClearCounter : BaseCounter
{

    //[SerializeField] private KitchenObjectsSO SpawnObj;

    //[SerializeField] private ClearCounter secondClearCounter;
    //[SerializeField] private bool testing;
    //private void Update()
    //{

    //    if (testing && Input.GetKeyDown(KeyCode.T))
    //    {

    //        if (kitchenObj != null)
    //        {
    //            kitchenObj.SetKitchenObjectParent(secondClearCounter);
    //        }
    //        else
    //        {
    //            Debug.Log("No kitchen obj");
    //        }
    //    }

    //}

    public override void Interact(PlayerControl player){
        if (!HasKitchenobject())
        {
            if (player.HasKitchenobject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            //Do nothing counter already has an object
            if (!player.HasKitchenobject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            
        }
    }


}