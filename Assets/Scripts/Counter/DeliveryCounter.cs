using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryKitchenPlate(out PlateKitchenObject plateKitchenObject ))
            {
                if (DeliveryManager.instance.DeliverRecipe(plateKitchenObject)){
                    player.GetKitchenObject().DestroyKitchenObject();
                }
                else
                {
                    Debug.Log("Wrong Recipe");
                }


            }
            else
            {
                Debug.Log("Recipe not completed");
            }
        }
    }
}
