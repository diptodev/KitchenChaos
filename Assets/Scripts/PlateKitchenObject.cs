using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
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
    protected override void Awake()
    {
        base.Awake();
        kitchenObjectsSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO _kitchenObjectSO)
    {
        if (validKitchenObjectSO.Contains(_kitchenObjectSO))
        {
            TryAddIngredientServerRpc(KitchenGameMultiplayer.Instance.GetKicthenOBjectIndexFromKitchenObjectSO(_kitchenObjectSO));
            return true;
        }
        return false;
    }
    [ServerRpc(RequireOwnership = false)]
    private void TryAddIngredientServerRpc(int kitchenObjectSOIndex)
    {
        TryAddIngredientClientRpc(kitchenObjectSOIndex);
    }
    [ClientRpc]
    private void TryAddIngredientClientRpc(int kitchenObjectSOIndex)
    {

        KitchenObjectSO _kitchenObjectSO = KitchenGameMultiplayer.Instance.GetKitchenObjectSOFromIndex(kitchenObjectSOIndex);
        kitchenObjectsSOList.Add(_kitchenObjectSO);
        OnIngredientAdded.Invoke(this, new OnIngredientAddedEventArgs()
        {
            kitchenObjectSO = _kitchenObjectSO
        });
    }
    public List<KitchenObjectSO> GetKitchenObjectList()
    {
        return kitchenObjectsSOList;
    }
}
