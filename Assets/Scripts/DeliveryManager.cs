using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager instance {  get; private set; }
    private List<KitchenReciepeSO> waitingRecipeSO;
    [SerializeField] private KitchenRecipeSOList mkitchenRecipeSO;
    private int currentWaitingRecipeNumber=0;
    private int maxWaitingRecipeNumber = 4;
    private float recipeSpawnedTime = 4;
    private float currentSpawnedTime = 0;
    private void Awake()
    {
        instance = this;
        waitingRecipeSO=new List<KitchenReciepeSO> ();
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
                KitchenReciepeSO kitchenReciepeSO = mkitchenRecipeSO.kitchenReciepeSOList[Random.Range(0,mkitchenRecipeSO.kitchenReciepeSOList.Count)];
                waitingRecipeSO.Add(kitchenReciepeSO);
                Debug.Log(kitchenReciepeSO.recipeName);
            }
           
        }

    }
    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
         List<KitchenObjectSO>kitchenObjectList=plateKitchenObject.GetKitchenObjectList();

        for (int i = 0; i < waitingRecipeSO.Count; i++)
        {
            KitchenReciepeSO kitchenReciepeSO = waitingRecipeSO[i];
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
                    waitingRecipeSO.RemoveAt(i);
                    currentWaitingRecipeNumber -= 1;
                    return true;
                }
                 
            }
           
        }
        return false;
    }
}

