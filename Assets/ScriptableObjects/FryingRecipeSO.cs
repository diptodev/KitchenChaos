using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO cooked;
    public KitchenObjectSO burned;
    public float maxCookedTime;
    public float maxRestingTime;
    public float maxBurningTime;
}
