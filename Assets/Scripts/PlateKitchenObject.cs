using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    private List<KitchenObjectSO> kitchenObjectsSOList;
    [SerializeField] private KitchenObjectSO[] validKitchenObjectSO;
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    private void Start()
    {
        kitchenObjectsSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO _kitchenObjectSO)
    {
        if (validKitchenObjectSO.Contains(_kitchenObjectSO))
        {
            kitchenObjectsSOList.Add(_kitchenObjectSO);
            OnIngredientAdded.Invoke(this, new OnIngredientAddedEventArgs()
            {
                kitchenObjectSO = _kitchenObjectSO
            });
            return true;
        }
        return false;
    }
    public List<KitchenObjectSO> GetKitchenObjectList()
    {
        return kitchenObjectsSOList;
    }
}
