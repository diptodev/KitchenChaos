using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{


    public override void Interact(Player player)
    {
        //ClearCounter has nothing
        if (!HasKitchenObject())
        {
            //Player has something
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetIKitchenObjectParent(this);
            }
            else
            {
                //Player has Nothing
            }
        }
        else
        {
            //Player Has Nothing
            if(HasKitchenObject())
            {
                //ClearCounter has something and give it to the player
                GetKitchenObject().SetIKitchenObjectParent(player);
            }
            else
            {
                //ClearCounter has nothing
            }
        }
    }

}
