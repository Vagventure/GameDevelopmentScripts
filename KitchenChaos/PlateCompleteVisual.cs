using System;
using UnityEngine;
using System.Collections.Generic;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectsSO_GameObject
    {
        public KitchenObjectsSO kitchenObjectsSO;
        public GameObject gameObject;
    }
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectsSO_GameObject> kitchenObjectsSOGameObjectList;
    void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectsSO_GameObject kitchenObjectsSO_GameObject in kitchenObjectsSOGameObjectList)
        {
            kitchenObjectsSO_GameObject.gameObject.SetActive(false);   
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectsSO_GameObject kitchenObjectsSO_GameObject in kitchenObjectsSOGameObjectList)
        {
            if(kitchenObjectsSO_GameObject.kitchenObjectsSO == e.kitchenObjectsSO)
            {
                kitchenObjectsSO_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
