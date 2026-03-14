using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO spawnObject;
    
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
