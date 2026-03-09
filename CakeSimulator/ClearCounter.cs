using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO spawnObject;
    [SerializeField] private Transform counterTopPosition;

    private KitchenObjects kitchenObject;
   public void Interact()
    {
        if(kitchenObject == null)
        {
            Debug.Log("Interaction Performed");
            kitchenObject =  KitchenObjects.SpawnKitchenObject(spawnObject,counterTopPosition);

        }

    }
}
