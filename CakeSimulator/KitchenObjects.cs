using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
   public static KitchenObjects SpawnKitchenObject(KitchenObjectsSO kitchenObjectsSO, Transform counterTopPoint)
    {
        Transform transform = Instantiate(kitchenObjectsSO.prefab, counterTopPoint);
        transform.localPosition = Vector3.zero;
        KitchenObjects kitchenObjects = transform.GetComponent<KitchenObjects>();

        return kitchenObjects;
        
    }

}
