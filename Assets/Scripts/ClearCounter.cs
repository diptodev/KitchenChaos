using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform topPoint;
    [SerializeField] private ClearCounter secondClearCounter;
   private KitchenObject kitchenObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)){
            kitchenObject.SetClearCounter(secondClearCounter);
        }
   
    }
    public void Interact()
    {

      if(kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefabVisuals, topPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.GetKitchenObjectSO() +" and the clearCounter is "+kitchenObject.GetClearCounter());
        }
    }
    public void SetKitchenObjectParent(KitchenObject kitchenObject)
    {

    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return topPoint;
    }
}
