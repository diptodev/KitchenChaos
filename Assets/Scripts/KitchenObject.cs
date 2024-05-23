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
    public void DestroyKitchenObject()
    {
        iKitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObject iKitchenObject)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefabVisuals);
        kitchenObjectTransform.localPosition = Vector3.zero;
      KitchenObject kitchenObject=  kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetIKitchenObjectParent(iKitchenObject);
        return kitchenObject;
    }
    public bool TryKitchenPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
             plateKitchenObject =this as PlateKitchenObject;
            return true;
        }
        plateKitchenObject = null;
        return false;
    }
}
