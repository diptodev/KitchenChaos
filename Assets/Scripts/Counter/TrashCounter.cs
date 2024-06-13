using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnItemTrashed;
    public static new void ClearStaticData()
    {
        OnItemTrashed = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroyKitchenObjectFromServer();
            OnItemTrashed.Invoke(this, EventArgs.Empty);
        }
    }
}
