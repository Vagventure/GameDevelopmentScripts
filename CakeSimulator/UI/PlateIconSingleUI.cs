using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image ingredientIcon;

    public void SetPlateIconUI(KitchenObjectsSO kitchenObjectsSO)
    {
        ingredientIcon.sprite = kitchenObjectsSO.sprite;
    }
}
