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
    }

    private void CuttingCounter_OnCuttingCounterProgressBar(object sender, CuttingCounter.OnCuttginCounterProgressBarEventArgs e)
    {
        progressBarImage.fillAmount = e.normalizedProgressBarValue;
    }
}
