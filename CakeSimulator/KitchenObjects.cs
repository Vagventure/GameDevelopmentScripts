using Unity.VisualScripting;
using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
   [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
   private IKitchenObjectParent kitchenObjectParent;
   public static KitchenObjects SpawnKitchenObject(KitchenObjectsSO kitchenObjectsSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform obj = Instantiate(kitchenObjectsSO.prefab);
        obj.localPosition = Vector3.zero;

        KitchenObjects kitchenObjects = obj.GetComponent<KitchenObjects>();
        kitchenObjects.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObjects;
        
    }

   public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
      
        this.kitchenObjectParent = kitchenObjectParent;

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetObjectFollowTransform();
        transform.localPosition = Vector3.zero;

    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public KitchenObjectsSO GetKitchenObjectsSO()
    {
        return kitchenObjectsSO;
    }
}
