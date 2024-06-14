using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingForOtherPlayersUI : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }
    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
