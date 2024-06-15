using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownTimerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textMeshUI;
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        transform.gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountDownActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Update()
    {
        textMeshUI.text = Mathf.Ceil(GameManager.Instance.GetCountDownTimer()).ToString();
    }
    void Show()
    {
        transform.gameObject.SetActive(true);

    }
    void Hide()
    {
        transform.gameObject.SetActive(false);
    }
}
