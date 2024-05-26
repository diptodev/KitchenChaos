using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerUI : MonoBehaviour
{

    [SerializeField] Transform container;
    [SerializeField] Transform recipeTemplate;
    private void Start()
    {
        DeliveryManager.instance.onRecipeSpawned += Delivery_onRecipeSpawned;
        DeliveryManager.instance.onRecipeCompleted += Delivery_onRecipeCompleted;
         recipeTemplate.gameObject.SetActive(false);
    }

    private void Delivery_onRecipeCompleted(object sender, System.EventArgs e)
    {
        Debug.Log("Deliver complete");
        updateVisual();    
    }

    private void Delivery_onRecipeSpawned(object sender, System.EventArgs e)
    {
       
        updateVisual();
    }

   
    public void updateVisual()
    {
        foreach (Transform child in container)
        {
            if (child==recipeTemplate)
            {
                continue; 
            }
            Destroy(child.gameObject);
        }
        foreach (KitchenReciepeSO kitchenReciepeSO in DeliveryManager.instance.GetKitchenRecipeSOList())
        {
           Transform recipeTransform= Instantiate(recipeTemplate, container);
           recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliverMangerSingleUI>().SetKitchenRecipeSO(kitchenReciepeSO);
        }
    }
}
