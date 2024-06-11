using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class KitchenObject : NetworkBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    private IKitchenObject iKitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }
    public void SetIKitchenObjectParent(IKitchenObject iKitchenObjectParent)
    {
        if (this.iKitchenObjectParent != null)
        {
            this.iKitchenObjectParent.ClearKitchenObject();
        }
        this.iKitchenObjectParent = iKitchenObjectParent;
        iKitchenObjectParent.SetKitchenObject(this);
        // transform.parent = iKitchenObjectParent.GetKitchenObjectFollowTransform();
        // transform.localPosition = Vector3.zero;
    }
    public IKitchenObject GetIKitchenObject()
    {
        return iKitchenObjectParent;
    }
    public void DestroyKitchenObject()
    {
        iKitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
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
}
