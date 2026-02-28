using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    [SerializeField] private KitchenObjectsSO SpawnObj;
    public event EventHandler OpenContainerTopDoor;
  
    public override void Interact(PlayerControl player)
    {
       KitchenObjects.SpawnKitchenObject(SpawnObj, player);
       OpenContainerTopDoor?.Invoke(this,EventArgs.Empty);
    }

}

