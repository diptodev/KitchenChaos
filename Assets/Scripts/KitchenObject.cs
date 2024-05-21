using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    private IKitchenObject iKitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }
    public void SetIKitchenObjectParent(IKitchenObject iKitchenObjectParent)
    {
        if (this.iKitchenObjectParent!=null)
        {
            this.iKitchenObjectParent.ClearKitchenObject();
        }
        this.iKitchenObjectParent = iKitchenObjectParent;
        iKitchenObjectParent.SetKitchenObject(this);
        transform.parent = iKitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObject GetIKitchenObject()
    {
        return iKitchenObjectParent;
    }
}
