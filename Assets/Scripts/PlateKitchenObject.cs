using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    private List<KitchenObjectSO> kitchenObjectsSOList;
    [SerializeField] private KitchenObjectSO[] validKitchenObjectSO;
    private void Start()
    {
        kitchenObjectsSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (validKitchenObjectSO.Contains(kitchenObjectSO))
        {
            kitchenObjectsSOList.Add(kitchenObjectSO);
            return true;
        }
        return false;
    }
}
