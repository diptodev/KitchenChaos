using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter,IProgressBarUI
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public event EventHandler OnCuttingRecipeInteract;
    public event EventHandler<IProgressBarUI.OnIProgressBarUIEventArgs> OnIProgressBarUI;
    public class OnCuttginCounterProgressBarEventArgs : EventArgs
    {
      public  float normalizedProgressBarValue;
        public string modeColor;
    }
    private KitchenObjectSO kitchenObjectSO;
    int currentCuttingProgress, maxCuttingProgress;
    Player player;
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
                    this.player = player;
                    player.GetKitchenObject().SetIKitchenObjectParent(this);
                    kitchenObjectSO = GetKitchenObject().GetKitchenObjectSO();
                }
                else
                {
                    kitchenObjectSO = null;
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
        if (kitchenObjectSO != null)
        {
            currentCuttingProgress++;
            maxCuttingProgress = GetSelectedCuttingRecipe(kitchenObjectSO).maxCuttingProgress;
            if (kitchenObjectSO==GetKitchenObject().GetKitchenObjectSO())
            {
                OnCuttingRecipeInteract.Invoke(this, EventArgs.Empty);
            }
            OnIProgressBarUI.Invoke(this, new IProgressBarUI.OnIProgressBarUIEventArgs
            {
                normalizedProgressBarValue = (float)currentCuttingProgress / maxCuttingProgress,
                modeColor = "burning"
            }) ;
            if (HasKitchenObject() && HasRecipeWithInput(kitchenObjectSO) && currentCuttingProgress >= maxCuttingProgress && !player.HasKitchenObject())
            {
                KitchenObjectSO outputKitchenObject = GetKitchenObjectSO(kitchenObjectSO);
                GetKitchenObject().DestroyKitchenObject();
                KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
               
            }

        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == kitchenObjectSO) return true;
        }
        return false;
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
