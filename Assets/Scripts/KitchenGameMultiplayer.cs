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
    public void StartHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += NetworkManager_ConnectionApprovalCallback;
        NetworkManager.Singleton.StartHost();
    }

    private void NetworkManager_ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        Debug.Log("Called");
        if (GameManager.Instance.IsGameWaitingToStart())
        {
            response.Approved = true;
        }
        else
        {
            response.Approved = false;
        }
    }

    public void StartCLient()
    {
        NetworkManager.Singleton.StartClient();
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

    public KitchenObjectSO GetKitchenObjectSOFromIndex(int indexOfKitchenObjectSO)
    {
        return kitchenObjectListSO.kitchenObjectListSO[indexOfKitchenObjectSO];
    }

    public int GetKicthenOBjectIndexFromKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        return kitchenObjectListSO.kitchenObjectListSO.IndexOf(kitchenObjectSO);
    }
    public void DestroyKitchenObjectInServer(KitchenObject kitchenObject)
    {
        DestroyKitchenObjectFromServerRpc(kitchenObject.NetworkObject);
    }
    [ServerRpc(RequireOwnership = false)]
    private void DestroyKitchenObjectFromServerRpc(NetworkObjectReference networkObjectReferenceKitchenObject)
    {
        networkObjectReferenceKitchenObject.TryGet(out NetworkObject networkObjectKitchenObject);
        KitchenObject kitchenObject = networkObjectKitchenObject.GetComponent<KitchenObject>();
        ClearKitchenObjectParentClientRpc(networkObjectReferenceKitchenObject);
        kitchenObject.DestroyKitchenObject();
    }
    [ClientRpc]
    private void ClearKitchenObjectParentClientRpc(NetworkObjectReference networkObjectReferenceKitchenObject)
    {
        networkObjectReferenceKitchenObject.TryGet(out NetworkObject networkObjectKitchenObject);
        KitchenObject kitchenObject = networkObjectKitchenObject.GetComponent<KitchenObject>();
        kitchenObject.ClearIkitchenParent();
    }
}
