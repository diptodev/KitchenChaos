using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{

    [SerializeField] KitchenObjectSO plateObjectSO;

    float plateSpawnInterval = 0;
    float maxPlateSpawnIntervalTime = 4;
    float plateSpawnAmount = 0;
    float maxPlateSpawnAmount = 4;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnSpawnedRemoved;
    private void Update()
    {
        plateSpawnInterval += Time.deltaTime;
        // Debug.Log(plateSpawnInterval);
        if (plateSpawnInterval >= maxPlateSpawnIntervalTime && plateSpawnAmount <= maxPlateSpawnAmount)
        {
            plateSpawnInterval = 0;
            plateSpawnAmount++;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }

    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (plateSpawnAmount > 0)
            {
                KitchenObject.SpawnKitchenObject(plateObjectSO, player);
                OnSpawnedRemoved?.Invoke(this, EventArgs.Empty);
                plateSpawnAmount--;
            }
        }
    }

}
