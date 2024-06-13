using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
                    KitchenObject.DestroyKitchenObjectFromServer(player.GetKitchenObject());

                    GameManager.Instance.setTimer();
                    OnDeliverySuccessServerRpc();
                }
                else
                {

                    OnDeliveryFailureServerRpc();
                }


            }
            else
            {

                //If recipe is not completed it goes here
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void OnDeliverySuccessServerRpc()
    {
        OnDeliverySuccessClientRpc();
    }
    [ClientRpc]
    private void OnDeliverySuccessClientRpc()
    {
        OnDeliverSuccess.Invoke(this, EventArgs.Empty);
    }
    [ServerRpc(RequireOwnership = false)]
    private void OnDeliveryFailureServerRpc()
    {
        OnDeliveryFailureClientRpc();
    }
    [ClientRpc]
    private void OnDeliveryFailureClientRpc()
    {

        OnDeliverFailure.Invoke(this, EventArgs.Empty);
    }
}
