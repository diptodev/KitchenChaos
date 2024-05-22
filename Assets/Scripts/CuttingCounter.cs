using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //ClearCounter has nothing

            if (player.HasKitchenObject())
            {
                //Player has something and give it to the clearcounter
                player.GetKitchenObject().SetIKitchenObjectParent(this);
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
        if (HasKitchenObject())
        {
            KitchenObjectSO outputKitchenObject=GetKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroyKitchenObject();
            KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
        }
        else
        {
            
        }
    }
    private KitchenObjectSO GetKitchenObjectSO(KitchenObjectSO intputKitchenObject)
    {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
          //  Debug.Log(cuttingRecipeSO.input +" = " + cuttingRecipeSO.output);
            if (cuttingRecipeSO.input == intputKitchenObject) return cuttingRecipeSO.output;
        }
        return null;
    }
     
}
