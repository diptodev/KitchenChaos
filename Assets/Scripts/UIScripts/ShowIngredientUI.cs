using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIngredientUI : MonoBehaviour
{
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] Transform iconTemplate;
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        iconTemplate.gameObject.SetActive(false);
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();

    }
    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;

            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);

            iconTransform.GetComponent<IconTemplate>().SetIngredientIcon(kitchenObjectSO);
            iconTransform.gameObject.SetActive(true);
        }
    }
}
