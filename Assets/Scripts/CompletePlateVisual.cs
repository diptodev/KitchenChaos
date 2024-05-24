using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletePlateVisual : MonoBehaviour
{
    [SerializeField]private PlateKitchenObject plateKitchenObject;
    
    [Serializable]
    public struct PlateKitchenIngredientGameObject
    {
      public  KitchenObjectSO kitchenObjectSO;
       public GameObject gameObject;
    }
    [SerializeField]
    private List<PlateKitchenIngredientGameObject> plateKitchenIngredientGameObjects;
    private void Start()
    {
         
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
       
       
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
       
        foreach (PlateKitchenIngredientGameObject plateKitchenIngredientGameObject in plateKitchenIngredientGameObjects)
        {
            
            if (plateKitchenIngredientGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                plateKitchenIngredientGameObject.gameObject.SetActive(true);
            }
        }
    }

   
}
