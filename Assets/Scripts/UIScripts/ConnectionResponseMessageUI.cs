using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionResponseMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI connectionResponseMessageText;
    [SerializeField] private Button buttonClose;
    private void Awake()
    {
        buttonClose.onClick.AddListener(Hide);
    }
    private void Start()
    {
        KitchenGameMultiplayer.Instance.OnFailedToConnect += KitchenGameMultiplayer_OnFailedToConnect;

        Hide();
    }

    private void KitchenGameMultiplayer_OnFailedToConnect(object sender, EventArgs e)
    {
        Show();
        connectionResponseMessageText.text = NetworkManager.Singleton.DisconnectReason;
        if (NetworkManager.Singleton.DisconnectReason == "")
        {
            connectionResponseMessageText.text = "Failed To Connect";
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
    private void OnDestroy()
    {
        KitchenGameMultiplayer.Instance.OnFailedToConnect -= KitchenGameMultiplayer_OnFailedToConnect;
    }
}
