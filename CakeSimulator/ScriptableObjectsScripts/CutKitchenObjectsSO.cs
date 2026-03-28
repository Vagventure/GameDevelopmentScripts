using UnityEngine;

[CreateAssetMenu()]
public class CutKitchenObjectsSO : ScriptableObject
{
    public KitchenObjectsSO currentKitchenObject;
    public int maxCutCount;
    public KitchenObjectsSO slicedKitchenObject;
}
