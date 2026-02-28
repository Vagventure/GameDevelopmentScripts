using UnityEngine;

public class TrashCounter : BaseCounter
{
public override void Interact(PlayerControl player)
    {
        if (player.HasKitchenobject())
        {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
