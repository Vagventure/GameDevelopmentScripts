using UnityEngine;

[CreateAssetMenu()]
public class CutKitchenObjectsSO : ScriptableObject
{
    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public int maxCutCount;
}
