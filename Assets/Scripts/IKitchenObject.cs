using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public interface IKitchenObject
{
    public void SetKitchenObject(KitchenObject kitchenObject);
    public void ClearKitchenObject();
    public KitchenObject GetKitchenObject();
    public Transform GetKitchenObjectFollowTransform();
    public NetworkObject GetNetworkObject();
}
