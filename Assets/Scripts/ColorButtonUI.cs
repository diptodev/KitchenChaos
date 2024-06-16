using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonUI : MonoBehaviour
{
    [SerializeField] private int colorId;
    [SerializeField] private Image image;
    [SerializeField] private GameObject selectedGameObject;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {

            KitchenGameMultiplayer.Instance.ChangePlayerColor(colorId);

        });
    }
    private void Start()
    {
        image.color = KitchenGameMultiplayer.Instance.GetPlayerColorByColorId(colorId);
        KitchenGameMultiplayer.Instance.OnPlayerDataListChanged += KitchenGameMultiplayer_OnPlayerDataListChanged;
        UpdateSelected();
    }

    private void KitchenGameMultiplayer_OnPlayerDataListChanged(object sender, EventArgs e)
    {
        UpdateSelected();
    }

    private void UpdateSelected()
    {
        if (KitchenGameMultiplayer.Instance.GetPlayerData().colorId == colorId)
        {
            selectedGameObject.SetActive(true);
        }
        else
        {
            selectedGameObject.SetActive(false);
        }
    }
}
