using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IProgressBar
{
    public event EventHandler<IProgressBar.OnProgressChangedEventArgs> OnProgressChanged;
    

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
            foreach (CutKitchenObjectsSO cutKitchenObjects in cutKitchenObjectsSOs)
            {
                if(cutKitchenObjects.input == player.GetKitchenObjects().GetKitchenObjectsSO())
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
                    break;
                }
               
            }

        }

    }

    public override void AltInteract(Player player)
    {
        if (HasKitchenObject() && HasRecipeSOWithInput(GetKitchenObjects().GetKitchenObjectsSO()))
        {
            if (player.HasKitchenObject())
            {
                //Do nothing counter alrready has an object and the player as well
            }
            else
            {
                //Peform cut operation
                cutProgress++;
                CutKitchenObjectsSO cutKitchenObjectsSO = GetCuttingRecipeSOWithInput(GetKitchenObjects().GetKitchenObjectsSO());
                OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                {
                    progressNormaliazed = (float)cutProgress / cutKitchenObjectsSO.maxCutCount
                });

                if (cutProgress >= cutKitchenObjectsSO.maxCutCount)
                {
                    GetKitchenObjects().DestroySelf();
                    KitchenObjects.SpawnKitchenObject(cutKitchenObjectsSO.output,this);
                    cutProgress = 0;
                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormaliazed = (float)cutProgress / cutKitchenObjectsSO.maxCutCount
                    });
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
   
}
