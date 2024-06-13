using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnContainerCounterInteract;
    public override void Interact(Player player)
    {

        if (!player.HasKitchenObject())
        {
            InteractContainerServerRpc();
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

        }
        else
        {
            // GetKitchenObject().SetIKitchenObjectParent(player);
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void InteractContainerServerRpc()
    {
        InteractContainerClientRpc();
    }
    [ClientRpc]
    private void InteractContainerClientRpc()
    {
        OnContainerCounterInteract?.Invoke(this, EventArgs.Empty);
    }
    public override void AlternateInteract()
    {

    }
}
