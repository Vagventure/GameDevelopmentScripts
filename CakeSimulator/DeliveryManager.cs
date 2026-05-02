using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFail;
    [SerializeField] private RecipeSOList recipeListSO;

    public static DeliveryManager Instance { get; private set; }
    
    private float recipeSpawnTimer = 0f;
    private float recipeSpawnTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int recipeDeliveredCount;

    private List<RecipeSO> waitingRecipeSOList;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        recipeSpawnTimer -= Time.deltaTime;

        if (recipeSpawnTimer <= 0f)
        {
            recipeSpawnTimer = recipeSpawnTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipeMax)
            {
            RecipeSO recipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
            waitingRecipeSOList.Add(recipeSO);

            //Debug.Log(recipeSO.recipeName);
            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            //Debug.Log("0");

            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            Debug.Log(waitingRecipeSO.kitchenObjectsSOList.Count + "vs" + plateKitchenObject.GetKitchenObjectsSOList().Count);

            int waitingRecipeSOCount = waitingRecipeSO.kitchenObjectsSOList.Count;

            if (waitingRecipeSOCount == plateKitchenObject.GetKitchenObjectsSOList().Count)
            {
                //Debug.Log("1");
                bool ingredientFound = false;
                bool plateContentsMatchsRecipe = true;
                foreach(KitchenObjectsSO recipeKitchenObjectsSO in waitingRecipeSO.kitchenObjectsSOList)
                {
                     ingredientFound = false;

                    foreach(KitchenObjectsSO plateKitchenObjectsSO in plateKitchenObject.GetKitchenObjectsSOList())
                    {
                        if(recipeKitchenObjectsSO == plateKitchenObjectsSO)
                        {
                            //Debug.Log("2");
                            Debug.Log(recipeKitchenObjectsSO + "vs" + plateKitchenObjectsSO);

                            ingredientFound = true;
                            break;
                        }
                       
                    }
                    if (!ingredientFound)
                    {
                        plateContentsMatchsRecipe = false;
                        break;
                    }
                }


                if (plateContentsMatchsRecipe)
                {
                    Debug.Log("Player delivered the correct recipe");
                    Debug.Log(waitingRecipeSO);
                    waitingRecipeSOList.Remove(waitingRecipeSO);

                    recipeDeliveredCount++;
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }

            }
        }
        Debug.Log("Player delivered the wrong item");
        OnDeliveryFail?.Invoke(this, EventArgs.Empty);
        //Debug.Log(plateKitchenObject.GetKitchenObjectsSOList().Count + "vs" + waitingRecipeSOList[0].kitchenObjectsSOList.Count);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetRecipeDeliveredCount()
    {
        return recipeDeliveredCount;
    }
}
