using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjectsSO", menuName = "Scriptable Objects/KitchenObjectsSO")]
public class KitchenObjectsSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objName;
}
