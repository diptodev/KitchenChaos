using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{


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

}
