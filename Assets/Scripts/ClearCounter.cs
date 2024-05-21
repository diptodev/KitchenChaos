using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform topPoint;
   public void Interact(ClearCounter clearCounter)
    {

      Transform tomatoTransform=  Instantiate(kitchenObjectSO.prefabVisuals, topPoint);
        tomatoTransform.localPosition=Vector3.zero; 
    }
}
