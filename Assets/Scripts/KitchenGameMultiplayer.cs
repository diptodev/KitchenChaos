using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenGameMultiplayer : NetworkBehaviour
{
    [SerializeField]
    private KitchenObjectListSO kitchenObjectListSO;
    public event EventHandler OnTryingToConnect;
    public event EventHandler OnFailedToConnect;
    public event EventHandler OnPlayerDataListChanged;
    public static KitchenGameMultiplayer Instance
    {
        get; private set;
    }
    public const int MAX_PLAYER_AMOUNT = 4;
    private NetworkList<PlayerData> playerDataNetworkList;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        playerDataNetworkList = new NetworkList<PlayerData>();
        playerDataNetworkList.OnListChanged += PlayerData_OnListChanged;
    }

    private void PlayerData_OnListChanged(NetworkListEvent<PlayerData> changeEvent)
    {
        OnPlayerDataListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void StartHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += NetworkManager_ConnectionApprovalCallback;
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClinetConnectedCallback;
        NetworkManager.Singleton.StartHost();
    }

    private void NetworkManager_OnClinetConnectedCallback(ulong connectedClientsId)
    {
        playerDataNetworkList.Add(new PlayerData
        {
            clientId = connectedClientsId,
        });
    }

    public void StartCLient()
    {
        OnTryingToConnect.Invoke(this, EventArgs.Empty);
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_ConnectionDisconnectCallback;
        NetworkManager.Singleton.StartClient();
    }
    private void NetworkManager_ConnectionDisconnectCallback(ulong clientId)
    {
        OnFailedToConnect?.Invoke(this, EventArgs.Empty);
    }

    private void NetworkManager_ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        if (SceneManager.GetActiveScene().name != SceneLoader.SceneState.CharacterSelectScene.ToString())
        {
            response.Approved = false;
            response.Reason = "Game Already Started";
            return;
        }
        if (NetworkManager.Singleton.ConnectedClientsIds.Count >= MAX_PLAYER_AMOUNT)
        {
            response.Approved = false;
            response.Reason = "Game Full";
            return;
        }
        response.Approved = true;

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
    public bool IsPlayerConnectedFromIndex(int playerIndex)
    {
        return playerIndex < playerDataNetworkList.Count;
    }

}
