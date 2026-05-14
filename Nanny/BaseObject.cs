using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Interact Performed");
    }
}
