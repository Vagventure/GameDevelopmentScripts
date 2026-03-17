using UnityEngine;

public class BaseCounter : MonoBehaviour
{
    [SerializeField] public Transform counterTopPosition;

    public virtual void Interact(Player player){}

  
}
