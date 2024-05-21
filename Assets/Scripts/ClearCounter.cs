using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter,IKitchenObject
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform topPoint;
 
   private KitchenObject kitchenObject;


    public override void Interact(IKitchenObject player)
    {

      if(kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefabVisuals, topPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetIKitchenObjectParent(this);
        }
        else
        {
            kitchenObject.SetIKitchenObjectParent(player);
        }
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return topPoint;
    }
}
