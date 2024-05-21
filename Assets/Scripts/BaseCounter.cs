using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour
{
    public virtual void Interact(IKitchenObject i)
    {
        Debug.LogError("Base Counter Interact call");
    }
}
