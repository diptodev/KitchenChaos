using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounterVisual : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private ClearCounter clearCouter;
    [SerializeField] private GameObject selectedCounter;
    void Start()
    {
        Player.playerInstance.OnSelectedCounterChanged += PlayerInstance_OnSelectedCounterChanged;
    }

    private void PlayerInstance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (clearCouter==e.selectedCounter)
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
        selectedCounter.SetActive(true);
    }
    private void HideClearCounterSelected()
    {
        selectedCounter.SetActive(false);
    }
}
