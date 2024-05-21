using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform tomato;
    [SerializeField] private Transform topPoint;
   public void Interact(ClearCounter clearCounter)
    {

      Transform tomatoTransform=  Instantiate(tomato, topPoint);
        tomatoTransform.localPosition=Vector3.zero; 
    }
}
