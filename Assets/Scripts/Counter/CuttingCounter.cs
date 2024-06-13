using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CuttingCounter : BaseCounter, IProgressBarUI
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public event EventHandler OnCuttingRecipeInteract;
    public event EventHandler<IProgressBarUI.OnIProgressBarUIEventArgs> OnIProgressBarUI;
    public static event EventHandler OnAnyCut;
    public static new void ClearStaticData()
    {
        OnAnyCut = null;
    }
    public class OnCuttginCounterProgressBarEventArgs : EventArgs
    {
        public float normalizedProgressBarValue;
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
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {

                    currentCuttingProgress = 0;
                    this.player = player;
                    KitchenObject kitchenObject = player.GetKitchenObject();
                    kitchenObject.SetIKitchenObjectParent(this);
                    kitchenObjectSO = kitchenObject.GetKitchenObjectSO();
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
                if (currentCuttingProgress >= maxCuttingProgress)
                {
                    GetKitchenObject().SetIKitchenObjectParent(player);
                }
            }
            else
            {
                if (player.GetKitchenObject().TryKitchenPlate(out PlateKitchenObject plateKitchenObject))
                {

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        KitchenObject.DestroyKitchenObjectFromServer(GetKitchenObject());

                    }
                }
                //ClearCounter has something but player has also something
            }
        }
    }
    public override void AlternateInteract()
    {
        if (kitchenObjectSO != null)
        {
            CuttingCounterAnimatorTriggerServerRpc();
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void CuttingCounterAnimatorTriggerServerRpc()
    {
        CuttingCounterAnimatorTriggerClientRpc();
    }
    [ClientRpc]
    private void CuttingCounterAnimatorTriggerClientRpc()
    {
        // KitchenObjectSO kitchenObjectSO = GetKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO());

        currentCuttingProgress++;
        KitchenObjectSO kitchenObjectSO = GetKitchenObject().GetKitchenObjectSO();
        CuttingRecipeSO cuttingRecipeSO = GetSelectedCuttingRecipe(kitchenObjectSO);

        if (cuttingRecipeSO == null) return;

        maxCuttingProgress = cuttingRecipeSO.maxCuttingProgress;
        OnCuttingRecipeInteract.Invoke(this, EventArgs.Empty);
        OnIProgressBarUI.Invoke(this, new IProgressBarUI.OnIProgressBarUIEventArgs
        {
            normalizedProgressBarValue = (float)currentCuttingProgress / maxCuttingProgress,
            modeColor = "burning"
        });
        if (HasKitchenObject() && HasRecipeWithInput(kitchenObjectSO) && currentCuttingProgress >= maxCuttingProgress && !player.HasKitchenObject())
        {
            KitchenObjectSO outputKitchenObject = GetKitchenObjectSO(kitchenObjectSO);
            KitchenObject.DestroyKitchenObjectFromServer(GetKitchenObject());
            KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
        }
        if (currentCuttingProgress <= maxCuttingProgress)
        {
            OnAnyCut.Invoke(this, EventArgs.Empty);
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
