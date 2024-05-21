using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObject
{
    private KitchenObject kitchenObject;
    [SerializeField] private Transform topPoint;
    public virtual void Interact(IKitchenObject i)
    {
        Debug.LogError("Base Counter Interact call");
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
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return topPoint;
    }
}
