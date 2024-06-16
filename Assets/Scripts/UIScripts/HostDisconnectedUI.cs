using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HostDisconnectedUIO : MonoBehaviour
{
    [SerializeField] private Button hostDisconnectedButton;
    private void Awake()
    {
        hostDisconnectedButton.onClick.AddListener(() =>
        {

        });
    }
    private void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectedCallback;
        Hide();
    }

    private void NetworkManager_OnClientDisconnectedCallback(ulong clientId)
    {
        if (clientId == NetworkManager.ServerClientId)
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
