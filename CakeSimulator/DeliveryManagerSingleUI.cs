using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform ingredientImage;

    private void Awake()
    {
        ingredientImage.gameObject.SetActive(false);
    }
    public void SetRecipe(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;
        foreach (Transform child in iconContainer)
        {
            if (child == ingredientImage) continue;
            Destroy(child.gameObject);
        }
    
        foreach (KitchenObjectsSO kitchenObjectsSO in recipeSO.kitchenObjectsSOList)
        {
            Debug.Log(kitchenObjectsSO.name);
           
            Transform recipeTransform = Instantiate(ingredientImage, iconContainer);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<Image>().sprite = kitchenObjectsSO.sprite;
        }
    }
}
