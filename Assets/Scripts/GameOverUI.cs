using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI textMeshProRecipeDelivered;
    void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        transform.gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            textMeshProRecipeDelivered.text=DeliveryManager.instance.GetTotalDeliverdRecipe().ToString();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        transform.gameObject.SetActive(true);
    }
    private void Hide()
    {
        transform.gameObject.SetActive(false);
    }
}
