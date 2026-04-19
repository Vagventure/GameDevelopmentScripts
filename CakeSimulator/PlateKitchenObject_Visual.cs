using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject_Visual : MonoBehaviour
{
    [Serializable]

    public struct kitchenObjectSO_GameObject
    {
        public KitchenObjectsSO kitchenObjectsSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<kitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Awake()
    {
        plateKitchenObject = GetComponentInParent<PlateKitchenObject>();
    }

    private void Start()
    {
        if (plateKitchenObject == null)
        {
            Debug.LogError("PlateKitchenObject is NULL!");
            return;
        }

        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (kitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectList)
        {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
       foreach(kitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectList)
       {
            Debug.Log("Loop ran");
            if(kitchenObjectSO_GameObject.kitchenObjectsSO == e.kitchenObjectsSO)
            {
                Debug.Log("if ran");

                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("else ran");

            }
        }
    }
}
