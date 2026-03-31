using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrashedSomething;
    public override void Interact(PlayerControl player)
    {
        if (player.HasKitchenobject())
        {
            player.GetKitchenObject().DestroySelf();
            OnTrashedSomething?.Invoke(this, EventArgs.Empty);
        }
    }
}
