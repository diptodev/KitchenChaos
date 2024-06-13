using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KitchenObject : NetworkBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    private IKitchenObject iKitchenObjectParent;
    private FollowKitchenObject followKitchenObject;
    protected virtual void Awake()
    {
        followKitchenObject = GetComponent<FollowKitchenObject>();
    }
    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }
    public void SetIKitchenObjectParent(IKitchenObject iKitchenObjectParent)
    {
        SetIKitchenObjectParentServerRpc(iKitchenObjectParent.GetNetworkObject());
    }
    [ServerRpc(RequireOwnership = false)]
    private void SetIKitchenObjectParentServerRpc(NetworkObjectReference iKitchenObjectNetworkRef)
    {
        SetIKitchenObjectParentClientRpc(iKitchenObjectNetworkRef);
    }
    [ClientRpc]
    private void SetIKitchenObjectParentClientRpc(NetworkObjectReference iKitchenObjectNetworkRef)
    {
        iKitchenObjectNetworkRef.TryGet(out NetworkObject iKitchenObjectNetworkObject);
        IKitchenObject iKitchenObjectParent = iKitchenObjectNetworkObject.GetComponent<IKitchenObject>();
        if (this.iKitchenObjectParent != null)
        {
            this.iKitchenObjectParent.ClearKitchenObject();
        }
        this.iKitchenObjectParent = iKitchenObjectParent;
        iKitchenObjectParent.SetKitchenObject(this);
        followKitchenObject.SetTargetKitchenTransform(iKitchenObjectParent.GetKitchenObjectFollowTransform());
    }
    public IKitchenObject GetIKitchenObject()
    {
        return iKitchenObjectParent;
    }
    public void DestroyKitchenObject()
    {

        Destroy(gameObject);
    }
    public void ClearIkitchenParent()
    {
        iKitchenObjectParent.ClearKitchenObject();
    }
    public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObject iKitchenObject)
    {

        KitchenGameMultiplayer.Instance.SpawnKitchenObject(kitchenObjectSO, iKitchenObject);
    }
    public bool TryKitchenPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        plateKitchenObject = null;
        return false;
    }

    public static void DestroyKitchenObjectFromServer(KitchenObject kitchenObject)
    {
        KitchenGameMultiplayer.Instance.DestroyKitchenObjectInServer(kitchenObject);
    }


}
