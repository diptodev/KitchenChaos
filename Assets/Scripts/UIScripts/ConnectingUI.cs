using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        KitchenGameMultiplayer.Instance.OnTryingToConnect += KitchenGameMultiplayer_OnTryToConnect;
        KitchenGameMultiplayer.Instance.OnFailedToConnect += KitchenGameMultiplayer_OnFailedToConnect;
        Hide();
    }

    private void KitchenGameMultiplayer_OnFailedToConnect(object sender, EventArgs e)
    {
        Hide();
    }

    private void KitchenGameMultiplayer_OnTryToConnect(object sender, EventArgs e)
    {
        Show();
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
        KitchenGameMultiplayer.Instance.OnTryingToConnect -= KitchenGameMultiplayer_OnTryToConnect;
        KitchenGameMultiplayer.Instance.OnFailedToConnect -= KitchenGameMultiplayer_OnFailedToConnect;
    }
}
