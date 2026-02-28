using System;
using UnityEngine;

public class TestCounter : BaseCounter
{
    public event EventHandler OpenTestCounterDoor;

    public override void Interact(PlayerControl player)
    {
        OpenTestCounterDoor?.Invoke(this, EventArgs.Empty);
    }
}
