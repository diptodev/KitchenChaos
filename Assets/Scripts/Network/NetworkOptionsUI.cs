using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkOptionsUI : MonoBehaviour
{
    [SerializeField] private Button startServer;
    [SerializeField] private Button startHost;
    [SerializeField] private Button startClient;
    private void Awake()
    {
        startServer.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            gameObject.SetActive(false);
        });
        startHost.onClick.AddListener(() =>
        {
            KitchenGameMultiplayer.Instance.StartHost();
            gameObject.SetActive(false);
        });
        startClient.onClick.AddListener(() =>
        {
            KitchenGameMultiplayer.Instance.StartCLient();
            gameObject.SetActive(false);
        });
    }
}
