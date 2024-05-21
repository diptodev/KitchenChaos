using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefabVisuals);
            kitchenObjectTransform.localPosition = Vector3.zero;
             kitchenObjectTransform.GetComponent<KitchenObject>().SetIKitchenObjectParent(this);
        }
        else
        {
           GetKitchenObject().SetIKitchenObjectParent(player);
        }
    }
 
}
