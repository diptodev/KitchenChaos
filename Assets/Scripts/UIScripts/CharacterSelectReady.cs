using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterSelectReady : NetworkBehaviour
{
    public static CharacterSelectReady instance { get; private set; }
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
        connectedClientActiveStatus[serverRpcParams.Receive.SenderClientId] = true;
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
}
