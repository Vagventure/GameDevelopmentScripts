using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemoved;


    private float spawnPlateTimer;
    private float spawnTimerPlateMax = 4f;
    private int plateSpawnAmount;
    private int plateSpawnAmountMax = 4;

    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnTimerPlateMax)
        {
            spawnPlateTimer = 0;
            if(plateSpawnAmount < plateSpawnAmountMax)
            {
                plateSpawnAmount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(PlayerControl player)
    {
        if (!player.HasKitchenobject())
        {
            //isnt holding something
            if (plateSpawnAmount > 0)
            {
                plateSpawnAmount--;
                KitchenObjects.SpawnKitchenObject(kitchenObjectsSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
