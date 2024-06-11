using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KitchenGameMultiplayer : NetworkBehaviour
{
    [SerializeField]
    private KitchenObjectListSO kitchenObjectListSO;
    public static KitchenGameMultiplayer Instance
    {
        get; private set;
    }
    private void Awake()
    {
        Instance = this;
    }
    public void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObject kitchenObjectParent)
    {
        int indexOfKitchenObjectSO = GetKicthenOBjectIndexFromKitchenObjectSO(kitchenObjectSO);
        SpawnKitchenObjectServerRpc(indexOfKitchenObjectSO, kitchenObjectParent.GetNetworkObject());
    }
    [ServerRpc(RequireOwnership = false)]
    private void SpawnKitchenObjectServerRpc(int indexOfKitchenObjectSO, NetworkObjectReference kitchenObjectParentNetworkObjectReference)
    {

        KitchenObjectSO kitchenObjectSO = GetKitchenObjectSOFromIndex(indexOfKitchenObjectSO);
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefabVisuals);
        NetworkObject kitchenObjectNetworkObject = kitchenObjectTransform.GetComponent<NetworkObject>();
        kitchenObjectNetworkObject.Spawn(true);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectParentNetworkObject);
        IKitchenObject kitchenObjectParent = kitchenObjectParentNetworkObject.GetComponent<IKitchenObject>();
        kitchenObject.SetIKitchenObjectParent(kitchenObjectParent);
    }

    private KitchenObjectSO GetKitchenObjectSOFromIndex(int indexOfKitchenObjectSO)
    {
        return kitchenObjectListSO.kitchenObjectListSO[indexOfKitchenObjectSO];
    }

    private int GetKicthenOBjectIndexFromKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        return kitchenObjectListSO.kitchenObjectListSO.IndexOf(kitchenObjectSO);
    }
}
