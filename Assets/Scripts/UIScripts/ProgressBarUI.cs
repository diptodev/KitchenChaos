using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    // [SerializeField] ProgressBarUI progressBarUI;
    [SerializeField] Image progressBarImage;
    [SerializeField] GameObject bar;
    [SerializeField] GameObject iProgressBarUIEventArgs;

    private IProgressBarUI progressBarUI;

    private void Start()
    {
        progressBarUI = iProgressBarUIEventArgs.GetComponent<IProgressBarUI>();
        progressBarUI.OnIProgressBarUI += ProgressBarUI_OnIProgressBarUI;
        progressBarImage.fillAmount = 0;
        Hide();

    }

    private void ProgressBarUI_OnIProgressBarUI(object sender, IProgressBarUI.OnIProgressBarUIEventArgs e)
    {
        progressBarImage.fillAmount = e.normalizedProgressBarValue;
        if (progressBarImage.fillAmount == 0 || progressBarImage.fillAmount == 1)
        {
            Hide();
        }
        else
        {

            Show(e.modeColor);

        }
    }
    private void Show(string color)
    {
        if (color == "burning")
        {
            progressBarImage.color = Color.yellow;
        }
        else if (color == "resting")
        {
            progressBarImage.color = Color.green;
        }
        else
        {
            progressBarImage.color = Color.red;
        }
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
