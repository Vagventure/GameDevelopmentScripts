using System;

public class TrashCounterScript : BaseCounter
{
    public static event EventHandler OnTrashObject;

    new public static void ResetStaticData()
    {
        OnTrashObject = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObjects().DestroySelf();
            OnTrashObject?.Invoke(this,EventArgs.Empty);
        }
    }
}
