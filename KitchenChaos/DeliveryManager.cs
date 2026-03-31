using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFail;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] public RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;

    private float recipeSpawnTimer = 0f;
    private float recipeSpawnTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private void Awake()
    {
        Instance = this;    
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        recipeSpawnTimer -= Time.deltaTime;

        if(recipeSpawnTimer <= 0f)
        {
            recipeSpawnTimer = recipeSpawnTimerMax;
            if(waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipe = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipe);
                Debug.Log(waitingRecipe);

                OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }
        }

    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectsSOList.Count == plateKitchenObject.GetKitchenObjectsSOList().Count)
            {
                bool plateContentsMatchsRecipe = true;
                foreach (KitchenObjectsSO recipeKitchenObjectsSO in waitingRecipeSO.kitchenObjectsSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectsSO plateKitchenObjectsSO in plateKitchenObject.GetKitchenObjectsSOList())
                    {
                        if (recipeKitchenObjectsSO == plateKitchenObjectsSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateContentsMatchsRecipe = false;
                    }

                    if (plateContentsMatchsRecipe)
                    {
                        Debug.Log("Player delivered the correct recipe");
                        waitingRecipeSOList.Remove(waitingRecipeSO);

                        OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                        OnDeliverySuccess?.Invoke(this,EventArgs.Empty);
                        return;
                    }
                }
            }
            
        }
        Debug.Log("Player delivered the wrong item");
        OnDeliveryFail?.Invoke(this,EventArgs.Empty);

    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

}
