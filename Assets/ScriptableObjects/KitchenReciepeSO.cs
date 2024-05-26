using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenReciepeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenRecipeSO;
    public string recipeName;
}
