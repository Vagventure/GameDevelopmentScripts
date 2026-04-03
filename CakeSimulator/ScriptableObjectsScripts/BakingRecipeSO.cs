using UnityEngine;

[CreateAssetMenu(fileName = "BakingRecipeObject", menuName = "BakingRecipeObjectSO")]
public class BakingRecipeSO : ScriptableObject
{
    public KitchenObjectsSO unBakedCakeBase;
    public KitchenObjectsSO bakedCakeBase;
    public KitchenObjectsSO overbakedCakeBase;
    public float bakeTimer;
    public float burnTimer;
}
