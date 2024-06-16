using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectPlayer : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject readyTextGameObject;
    private void Start()
    {
        KitchenGameMultiplayer.Instance.OnPlayerDataListChanged += KitchenGameMultiplayer_OnPlayerDataListChanged;
        CharacterSelectReady.instance.OnReadyChanged += CharacterSelectReady_OnReadyChanged;
        UpdateVisual();
        readyTextGameObject.SetActive(false);
    }

    private void CharacterSelectReady_OnReadyChanged(object sender, EventArgs e)
    {
        readyTextGameObject.SetActive(true);
    }

    private void KitchenGameMultiplayer_OnPlayerDataListChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        if (KitchenGameMultiplayer.Instance.IsPlayerConnectedFromIndex(playerIndex))
        {
            Show();
        }
        else
        {
            Hide();
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
