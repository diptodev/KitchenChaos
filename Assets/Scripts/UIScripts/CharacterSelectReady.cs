using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterSelectReady : NetworkBehaviour
{
    public static CharacterSelectReady instance { get; private set; }
    public event EventHandler OnReadyChanged;
    private Dictionary<ulong, bool> connectedClientActiveStatus;
    private void Awake()
    {
        connectedClientActiveStatus = new Dictionary<ulong, bool>();
        instance = this;
    }
    public void SetPlayerReady()
    {
        LocalPlayerReadyServerRpc();
    }
    [ServerRpc(RequireOwnership = false)]
    private void LocalPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        LocalPlayerReadyClientRpc(serverRpcParams.Receive.SenderClientId);
        bool allClientIsReady = true;
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!connectedClientActiveStatus.ContainsKey(clientId) || !connectedClientActiveStatus[clientId])
            {
                allClientIsReady = false;
                break;
            }
        }
        if (allClientIsReady)
        {
            SceneLoader.LoadSceneFromNetwork(SceneLoader.SceneState.GameScene);
        }
    }
    [ClientRpc]
    private void LocalPlayerReadyClientRpc(ulong clientId)
    {
        connectedClientActiveStatus[clientId] = true;
        OnReadyChanged?.Invoke(this, EventArgs.Empty);
    }
}
