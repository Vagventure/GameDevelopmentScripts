using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

    private IKitchenObjectParent kitchenObjectParent;
    
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {

        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
   
        this.kitchenObjectParent = kitchenObjectParent;
       
        if (kitchenObjectParent.HasKitchenobject())
        {
            Debug.Log("Counter already has an object");
        }
       
        kitchenObjectParent.SetkitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        
        transform.localPosition = Vector3.zero;
        Debug.Log("World Pos: " + transform.position);
        //Debug.Log("Parent: " + transform.parent.name);


    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public KitchenObjectsSO GetKitchenObjectSO()
    {
        return kitchenObjectsSO;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObjects SpawnKitchenObject(KitchenObjectsSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform obj = Instantiate(kitchenObjectSO.prefab);
        KitchenObjects kitchenObject = obj.GetComponent<KitchenObjects>();
        
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}
