using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnContainerCounterInteract;
    public override void Interact(Player player)
    {

        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnContainerCounterInteract?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            // GetKitchenObject().SetIKitchenObjectParent(player);
        }
    }
    public override void AlternateInteract()
    {

    }
}
