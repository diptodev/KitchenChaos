using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverMangerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeTitle;
    [SerializeField] private Transform iconImage;
    [SerializeField] private Transform iconContainer;
  public void SetKitchenRecipeSO(KitchenReciepeSO kitchenReciepeSO)
    {
        recipeTitle.text = kitchenReciepeSO.recipeName;
        foreach (Transform child in iconContainer)
        {
            if (child==iconImage)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in kitchenReciepeSO.kitchenRecipeSO)
        {
          Transform transform=  Instantiate(iconImage, iconContainer);
            transform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
            transform.gameObject.SetActive(true);
        }
    }
}
