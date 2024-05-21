using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    int counter = 1;
   public void Interact(ClearCounter clearCounter)
    {
        
        Debug.Log("Interact" +clearCounter);
    }
}
