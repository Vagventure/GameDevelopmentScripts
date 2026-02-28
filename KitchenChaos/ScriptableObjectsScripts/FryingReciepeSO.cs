using UnityEngine;

[CreateAssetMenu(fileName = "FryingObjectsSO", menuName = "Scriptable Objects/FryingObjectsSO")]
public class FryingReciepeSO : ScriptableObject
{
    public KitchenObjectsSO unCookedMeatPatty;
    public KitchenObjectsSO cookedMeatPatty;
    public KitchenObjectsSO burnedMeatPatty;
    public float fryingTimerCount;
    public float burnedTimerCount;
}
