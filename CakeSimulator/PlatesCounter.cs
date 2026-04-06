using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectsSO spawnObject;

    private float plateSpawnTimer;
    private float platerSpawnTimerMax = 4f;
    private int spawnPlateCount;
    private int maxAllowedPlateCount = 5;

    private void Update()
    {
        plateSpawnTimer += Time.deltaTime;
        if(plateSpawnTimer > platerSpawnTimerMax)
        {
            plateSpawnTimer = 0;
            if(spawnPlateCount < maxAllowedPlateCount)
            {
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
                spawnPlateCount++;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
           if(spawnPlateCount > 0)
            {
                spawnPlateCount--;

                KitchenObjects.SpawnKitchenObject(spawnObject, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }

        }

    }
}
