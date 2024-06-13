using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameOverUI : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI textMeshProRecipeDelivered;
    void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        transform.gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged(object sender, GameManager.OnStateChangedValue e)
    {
        Debug.Log(e.gameState);
        if (e.gameState == GameManager.GameState.GameOver)
        {
            Show();
            textMeshProRecipeDelivered.text = DeliveryManager.instance.GetTotalDeliverdRecipe().ToString();
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
