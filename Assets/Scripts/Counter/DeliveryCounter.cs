using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{

    public event EventHandler OnDeliverSuccess;
    public event EventHandler OnDeliverFailure;
    public static new void ClearStaticData()
    {
        // OnDeliverFailure = null;
        // OnDeliverSuccess = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryKitchenPlate(out PlateKitchenObject plateKitchenObject))
            {
                if (DeliveryManager.instance.DeliverRecipe(plateKitchenObject))
                {
                    player.GetKitchenObject().DestroyKitchenObject();
                    OnDeliverSuccess.Invoke(this, EventArgs.Empty);
                    GameManager.Instance.setTimer();
                }
                else
                {

                    OnDeliverFailure.Invoke(this, EventArgs.Empty);
                }


            }
            else
            {
                //If recipe is not completed it goes here
            }
        }
    }
}
