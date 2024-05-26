using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static event EventHandler OnDeliverSuccess;
    public static event EventHandler OnDeliverFailure;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryKitchenPlate(out PlateKitchenObject plateKitchenObject ))
            {
                if (DeliveryManager.instance.DeliverRecipe(plateKitchenObject)){
                    player.GetKitchenObject().DestroyKitchenObject();
                    OnDeliverSuccess.Invoke(this,EventArgs.Empty);
                }
                else
                {
                    Debug.Log("Wrong Recipe");
                    OnDeliverFailure.Invoke(this,EventArgs.Empty);
                }


            }
            else
            {
                Debug.Log("Recipe not completed");
            }
        }
    }
}
