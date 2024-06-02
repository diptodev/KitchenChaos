using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BaseCounter : NetworkBehaviour, IKitchenObject
{
    private KitchenObject kitchenObject;
    [SerializeField] private Transform topPoint;
    public static event EventHandler OnPickedUpSomething;
    public static event EventHandler OnDropedSomething;
    public static void ClearStaticData()
    {
        OnDropedSomething = null;
        OnDropedSomething = null;
    }

    public virtual void Interact(Player player)
    {
        Debug.LogError("Base Counter Interact call");
    }
    public virtual void AlternateInteract()
    {

        Debug.LogError("Base Counter AlternateInteract call");
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (this.kitchenObject.GetIKitchenObject() is Player)
        {
            OnPickedUpSomething.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnDropedSomething.Invoke(this, EventArgs.Empty);
        }

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
