using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private KitchenObjectSO kitchenObjectSO;
    int currentCuttingProgress, maxCuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //ClearCounter has nothing

            if (player.HasKitchenObject())
            {
                //Player has something and give it to the clearcounter
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                    currentCuttingProgress= 0;
                    player.GetKitchenObject().SetIKitchenObjectParent(this);
                    kitchenObjectSO = GetKitchenObject().GetKitchenObjectSO();
                }
            }
            else
            {
                //Player has Nothing
            }
        }

        else
        {
            //ClearCounter Has SomeThing
            if (!player.HasKitchenObject())
            {
                //ClearCounter has something and give it to the player
                GetKitchenObject().SetIKitchenObjectParent(player);
            }
            else
            {
                //ClearCounter has something but player has also something
            }
        }
    }
    public override void AlternateInteract()
    {
        currentCuttingProgress++;
        maxCuttingProgress = GetSelectedCuttingRecipe(kitchenObjectSO).maxCuttingProgress;
        if (HasKitchenObject() && HasRecipeWithInput(kitchenObjectSO)&&currentCuttingProgress>=maxCuttingProgress)
        {
            KitchenObjectSO outputKitchenObject=GetKitchenObjectSO(kitchenObjectSO);
            GetKitchenObject().DestroyKitchenObject();
            KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
        }
        else
        {
            
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        return GetSelectedCuttingRecipe(kitchenObjectSO).input==kitchenObjectSO;
    }
    private KitchenObjectSO GetKitchenObjectSO(KitchenObjectSO inputKitchenObject)
    {
        return GetSelectedCuttingRecipe(inputKitchenObject).output;
    }
    private CuttingRecipeSO GetSelectedCuttingRecipe(KitchenObjectSO inputKitchenObject)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObject) return cuttingRecipeSO;
        }
        return null;
    }
     
}
