using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionProgressBarContainerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI connectionPersentage;
    [SerializeField] private Image progressBarUI;
    [SerializeField] private int maxConnectionAttempt;
    private int currentConnectionAttempt = 0;
    private float progressBarUITimeout = 0;
    private float timeOut;
    // Update is called once per frame
    void Update()
    {
        if (currentConnectionAttempt <= maxConnectionAttempt)
        {
            timeOut += Time.deltaTime;
            progressBarUITimeout += Time.deltaTime;
            progressBarUI.fillAmount = progressBarUITimeout / maxConnectionAttempt;
            connectionPersentage.text = $"{Mathf.Ceil((progressBarUITimeout / maxConnectionAttempt) * 100)} %";
            if (timeOut >= 1)
            {
                currentConnectionAttempt++;
                timeOut = 0;
            }

        }

    }
}
