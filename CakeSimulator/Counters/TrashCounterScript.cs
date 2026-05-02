using System;

public class TrashCounterScript : BaseCounter
{
    public static event EventHandler OnTrashObject;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObjects().DestroySelf();
            OnTrashObject?.Invoke(this,EventArgs.Empty);
        }
    }
}
