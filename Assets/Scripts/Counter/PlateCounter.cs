using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    private NetworkVariable<int> plateSpawnAmount = new NetworkVariable<int>(0, readPerm: NetworkVariableReadPermission.Everyone, writePerm: NetworkVariableWritePermission.Owner);
    [SerializeField] KitchenObjectSO plateObjectSO;

    float plateSpawnInterval = 0;
    float maxPlateSpawnIntervalTime = 4;
    //float plateSpawnAmount = 0;
    int totalPlateSpawned = 0;
    float maxPlateSpawnAmount = 4;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnSpawnedRemoved;
    private void Update()
    {
        if (!IsServer) return;
        plateSpawnInterval += Time.deltaTime;
        if (plateSpawnInterval > maxPlateSpawnIntervalTime)
        {
            plateSpawnInterval = 0;
            if (GameManager.Instance.IsGamePlaying() && plateSpawnAmount.Value <= maxPlateSpawnAmount)
            {
                SpawnPlateServerRpc();

            }
        }
        // Debug.Log(plateSpawnInterval);


    }
    [ServerRpc]
    private void SpawnPlateServerRpc()
    {
        SpawnPlateClientRpc();
        plateSpawnAmount.Value++;
    }
    [ClientRpc]
    private void SpawnPlateClientRpc()
    {

        int remainingSpawned = math.abs(plateSpawnAmount.Value - totalPlateSpawned);
        //OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        for (int i = 0; i < remainingSpawned; i++)
        {
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }
        totalPlateSpawned = plateSpawnAmount.Value;
    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (plateSpawnAmount.Value > 0)
            {
                KitchenObject.SpawnKitchenObject(plateObjectSO, player);
                RemovePlateServerRpc();
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void RemovePlateServerRpc()
    {
        plateSpawnAmount.Value--;

        RemovePlateClientRpc();
    }
    [ClientRpc]
    private void RemovePlateClientRpc()
    {
        totalPlateSpawned--;
        OnSpawnedRemoved?.Invoke(this, EventArgs.Empty);
    }
}
