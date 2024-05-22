using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
   // [SerializeField] ProgressBarUI progressBarUI;
    [SerializeField] Image progressBarImage;
    [SerializeField] CuttingCounter cuttingCounter;
    private void Start()
    {
        cuttingCounter.OnCuttingCounterProgressBar += CuttingCounter_OnCuttingCounterProgressBar;
        progressBarImage.fillAmount = 0;
        Hide();
       
    }
    private void CuttingCounter_OnCuttingCounterProgressBar(object sender, CuttingCounter.OnCuttginCounterProgressBarEventArgs e)
    {
        progressBarImage.fillAmount = e.normalizedProgressBarValue;
        if (progressBarImage.fillAmount == 0 || progressBarImage.fillAmount == 1)
        {
            Hide();
        }
        else
        {
            Show();
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
