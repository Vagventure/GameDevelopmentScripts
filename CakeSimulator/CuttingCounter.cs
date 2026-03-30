using UnityEngine;

public class CuttingCounter : BaseCounter, IKitchenObjectParent
{
  
    private KitchenObjects kitchenObject;

    private int cutProgress;
    [SerializeField] private CutKitchenObjectsSO[] cutKitchenObjectsSOs;

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
                cutProgress = 0;
            }
            else
            {
                //Do nothing
            }

        }

    }

    public override void AltInteract(Player player)
    {
        if (HasKitchenObject() && HasRecipeSOWithInput(kitchenObject.GetKitchenObjectsSO()))
        {
            if (player.HasKitchenObject())
            {
                //Do nothing counter alrready has an object and the player as well
            }
            else
            {
                //Peform cut operation
                cutProgress++;
                CutKitchenObjectsSO cutKitchenObjectsSO = GetCuttingRecipeSOWithInput(kitchenObject.GetKitchenObjectsSO());

                if(cutProgress >= cutKitchenObjectsSO.maxCutCount)
                {
                    kitchenObject.DestroySelf();
                    KitchenObjects.SpawnKitchenObject(cutKitchenObjectsSO.output,this);
                    cutProgress = 0;
                }

                
            }
        }
    }

    public CutKitchenObjectsSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (CutKitchenObjectsSO cutKitchenObject in cutKitchenObjectsSOs) 
        {
            if(cutKitchenObject.input == inputKitchenObjectSO)
            {
                return cutKitchenObject;
            }
        }
        return null;
 

    }

    public bool HasRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (CutKitchenObjectsSO cutKitchenObject in cutKitchenObjectsSOs)
        {
            if (cutKitchenObject.input == inputKitchenObjectSO)
            {
                return true;
            }
        }
        return false;


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
