using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform container;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private KitchenObjectsSO bakeCounter;
    [SerializeField] private GameObject bakeCounter2;

    [SerializeField] private KitchenObjectsSO unBakedCakeBase;
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }


    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);   
        }
       
        foreach(KitchenObjectsSO kitchenObjectsSO in plateKitchenObject.GetKitchenObjectsSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate,container);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetPlateIconUI(kitchenObjectsSO);
        }
    }
}
