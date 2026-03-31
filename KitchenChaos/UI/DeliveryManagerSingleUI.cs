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

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child == ingredientImage) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectsSO kitchenObjectsSO in recipeSO.kitchenObjectsSOList)
        {
            Transform iconTransform = Instantiate(ingredientImage,iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectsSO.sprite;
        }
    }
}
