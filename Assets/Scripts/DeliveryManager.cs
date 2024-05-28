using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DeliveryManager : MonoBehaviour
{
    private int totalRecipeDelivered=0;
    public event EventHandler onRecipeSpawned;
    public event EventHandler onRecipeCompleted;
    public static DeliveryManager instance {  get; private set; }
    private List<KitchenReciepeSO> waitingRecipeSOList;
    [SerializeField] private KitchenRecipeSOList mkitchenRecipeSO;
    private int currentWaitingRecipeNumber=0;
    private int maxWaitingRecipeNumber = 4;
    private float recipeSpawnedTime = 4;
    private float currentSpawnedTime = 0;
    private void Awake()
    {
        instance = this;
        waitingRecipeSOList=new List<KitchenReciepeSO> ();
    }
    private void Update()
    {
        if (currentWaitingRecipeNumber<maxWaitingRecipeNumber)
        {
            currentSpawnedTime += Time.deltaTime;
            if (currentSpawnedTime >= recipeSpawnedTime)
            {
                currentWaitingRecipeNumber++;
                currentSpawnedTime=0;
                KitchenReciepeSO kitchenReciepeSO = mkitchenRecipeSO.kitchenReciepeSOList[UnityEngine.Random.Range(0,mkitchenRecipeSO.kitchenReciepeSOList.Count)];
                waitingRecipeSOList.Add(kitchenReciepeSO);
                onRecipeSpawned.Invoke(this, EventArgs.Empty);
            }
           
        }

    }
    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
         List<KitchenObjectSO>kitchenObjectList=plateKitchenObject.GetKitchenObjectList();

        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            KitchenReciepeSO kitchenReciepeSO = waitingRecipeSOList[i];
            if (kitchenObjectList.Count==kitchenReciepeSO.kitchenRecipeSO.Count)
            {
                bool completeIngredientFound = true;
                foreach (KitchenObjectSO kitchenObjectSO in kitchenReciepeSO.kitchenRecipeSO)
                {
                    bool ingredientMatch = false;
                    foreach (KitchenObjectSO mplateKitchenObject in kitchenObjectList)
                    {
                        if (kitchenObjectSO == mplateKitchenObject)
                        {
                            ingredientMatch = true;
                            break;
                        }
                    }
                    if (!ingredientMatch)
                    {
                        completeIngredientFound = false;
                    }

                }
                if (completeIngredientFound)
                {
                    
                    waitingRecipeSOList.RemoveAt(i);
                    currentWaitingRecipeNumber -= 1;
                    totalRecipeDelivered++;
                    onRecipeCompleted.Invoke(this, EventArgs.Empty);
                    return true;
                }
                 
            }
           
        }
        return false;
    }
    public List<KitchenReciepeSO> GetKitchenRecipeSOList()
    {
        return waitingRecipeSOList;
    }
    public int GetTotalDeliverdRecipe()
    {
        return totalRecipeDelivered;
    }
}

