using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO plateObjectSO;
    [SerializeField] Transform SpawnedPoint;
    float plateSpawnInterval = 0;
    float maxPlateSpawnIntervalTime = 4;
    float plateSpawnAmount = 0;
    float maxPlateSpawnAmount = 4;
    private void Update()
    {
        plateSpawnInterval += Time.deltaTime;
       // Debug.Log(plateSpawnInterval);
        if (plateSpawnInterval>= maxPlateSpawnIntervalTime && plateSpawnAmount<=maxPlateSpawnAmount)
        {
            plateSpawnInterval = 0;
            plateSpawnAmount++;
            Instantiate(plateObjectSO.prefabVisuals, SpawnedPoint);
        }

    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(plateObjectSO, this);
            GetKitchenObject().SetIKitchenObjectParent(this);
            GetKitchenObject().SetIKitchenObjectParent(player);
            
          //  GetKitchenObject().DestroyKitchenObject();
        }
    }

}
