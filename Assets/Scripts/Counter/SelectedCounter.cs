using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedCounter;



    void Start()
    {
        if (Player.localPlayerInstance != null)
        {
            Player.localPlayerInstance.OnSelectedCounterChanged += PlayerInstance_OnSelectedCounterChanged;
        }
        else
        {
            Player.OnAnyPlayerSpawned += LocalPlayerInstanceEvent;
        }

    }
    private void LocalPlayerInstanceEvent(object sender, EventArgs e)
    {
        if (Player.localPlayerInstance != null)
        {
            Player.localPlayerInstance.OnSelectedCounterChanged -= PlayerInstance_OnSelectedCounterChanged;
            Player.localPlayerInstance.OnSelectedCounterChanged += PlayerInstance_OnSelectedCounterChanged;
        }
    }

    private void PlayerInstance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (baseCounter == e.selectedCounter)
        {
            ShowClearCounterSelected();
        }
        else
        {
            HideClearCounterSelected();
        }
    }
    private void ShowClearCounterSelected()
    {
        foreach (GameObject selectedCounterObject in selectedCounter)
        {
            selectedCounterObject.SetActive(true);
        }

    }
    private void HideClearCounterSelected()
    {
        foreach (GameObject selectedCounterObject in selectedCounter)
        {
            selectedCounterObject.SetActive(false);
        }
    }

}
