using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failureColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failureSprite;
    [SerializeField] private DeliveryCounter deliveryCounter;
    private Animator animator;
    private const string POP_UP = "Popup";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
        deliveryCounter.OnDeliverSuccess += DeliveryManager_onRecipeCompleted;
        deliveryCounter.OnDeliverFailure += DeliverManager_onRecipeFailure;
    }
    private void DeliveryManager_onRecipeCompleted(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "Delivery\nSuccess";
        animator.SetTrigger(POP_UP);
    }
    private void DeliverManager_onRecipeFailure(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        backgroundImage.color = failureColor;
        iconImage.sprite = failureSprite;
        messageText.text = "Delivery\nFailure";
        animator.SetTrigger(POP_UP);
    }
}
