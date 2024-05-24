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
                player.GetKitchenObject().DestroyKitchenObject();
                Debug.Log("Recipe Deliver Success");

            }
            else
            {
                Debug.Log("Wrong Recipe ");
            }
        }
    }
}
