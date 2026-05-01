using UnityEngine;

public class TrashCounterScript : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObjects().DestroySelf();
        }
    }
}
