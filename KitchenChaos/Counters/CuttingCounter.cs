using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter, IProgressBar
{
    public event EventHandler<IProgressBar.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
   
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSO;

    private int cuttingProgress;
    public override void Interact(PlayerControl player)
    {
        if (!HasKitchenobject())
        {
            if (player.HasKitchenobject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0;
                }
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

    public override void AltInteract(PlayerControl player)
    {
        if (HasKitchenobject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //Cut the into slices
            cuttingProgress++;

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
            {
                progressNormaliazed = (float)cuttingProgress / cuttingRecipeSO.numOfCuts
            });

            OnCut?.Invoke(this, EventArgs.Empty);

            if (cuttingProgress >= cuttingRecipeSO.numOfCuts)
            {
                KitchenObjectsSO outputKitchenObjectSO = GetOutputForInputSO(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObjects.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
           
        }
        
    }

    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectsSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectsSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectsSO GetOutputForInputSO(KitchenObjectsSO inputKitchenObjectSO)
    {
        CuttingRecipeSO outputKitchenObjectSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if(outputKitchenObjectSO != null)
        {
            return outputKitchenObjectSO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO) 
    { 
        foreach (CuttingRecipeSO cuttingRecipe in cuttingRecipeSO)
        {
            if (cuttingRecipe.input == inputKitchenObjectSO)
            {
                return cuttingRecipe;
            }
        }
        return null;
    }
}
