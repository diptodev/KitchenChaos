using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;


public class DeliveryManager : NetworkBehaviour
{
    private int totalRecipeDelivered = 0;
    public event EventHandler onRecipeSpawned;
    public event EventHandler onRecipeCompleted;
    public event EventHandler onRecipeFailure;
    public static DeliveryManager instance { get; private set; }
    private List<KitchenReciepeSO> waitingRecipeSOList;
    [SerializeField] private KitchenRecipeSOList mkitchenRecipeSO;
    private int currentWaitingRecipeNumber = 0;
    private int maxWaitingRecipeNumber = 4;
    private float recipeSpawnedTime = 4;
    private float currentSpawnedTime = 0;
    private void Awake()
    {
        instance = this;
        waitingRecipeSOList = new List<KitchenReciepeSO>();

    }
    public override void OnNetworkSpawn()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameStart())
        {
            gameObject.SetActive(true);
            Debug.Log("Started Game");
        }
    }
    private void Update()
    {
        if (!IsServer)
        {
            return;
        }
        if (currentWaitingRecipeNumber < maxWaitingRecipeNumber)
        {
            currentSpawnedTime += Time.deltaTime;
            if (currentSpawnedTime >= recipeSpawnedTime)
            {
                currentWaitingRecipeNumber++;
                currentSpawnedTime = 0;
                int waitingRecipeSOIndex = UnityEngine.Random.Range(0, mkitchenRecipeSO.kitchenReciepeSOList.Count);
                SpwanNewWaitingRecipeSOServerRpc(waitingRecipeSOIndex);
            }

        }

    }
    [ServerRpc]
    private void SpwanNewWaitingRecipeSOServerRpc(int waitingRecipeSOIndex)
    {
        SpawnNewWaitingRecipeSOClientRpc(waitingRecipeSOIndex);
    }
    [ClientRpc]
    private void SpawnNewWaitingRecipeSOClientRpc(int waitingRecipeSOIndex)
    {
        KitchenReciepeSO kitchenReciepeSO = mkitchenRecipeSO.kitchenReciepeSOList[waitingRecipeSOIndex];
        waitingRecipeSOList.Add(kitchenReciepeSO);
        onRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> kitchenObjectList = plateKitchenObject.GetKitchenObjectList();

        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            KitchenReciepeSO kitchenReciepeSO = waitingRecipeSOList[i];
            if (kitchenObjectList.Count == kitchenReciepeSO.kitchenRecipeSO.Count)
            {
                bool completeIngredientFound = true;
                foreach (KitchenObjectSO kitchenObjectSO in kitchenReciepeSO.kitchenRecipeSO)
                {
                    bool ingredientMatch = false;
                    foreach (KitchenObjectSO mplateKitchenObject in kitchenObjectList)
                    {
                        if (kitchenObjectSO == mplateKitchenObject)
                        {
                            ingredientMatch = true;
                            break;
                        }
                    }
                    if (!ingredientMatch)
                    {
                        completeIngredientFound = false;
                    }

                }
                if (completeIngredientFound)
                {

                    DeliverSuccessRecipeServerRpc(i);
                    return true;
                }
                else
                {
                    DeliverFailureRecipeServerRpc();
                }

            }

        }
        return false;
    }
    [ServerRpc(RequireOwnership = false)]
    private void DeliverSuccessRecipeServerRpc(int deliveredRecipeIndex)
    {
        DeliverSuccessRecipeClientRpc(deliveredRecipeIndex);
    }
    [ClientRpc]
    private void DeliverSuccessRecipeClientRpc(int deliveredRecipeIndex)
    {
        Debug.Log("Delivery Success");
        waitingRecipeSOList.RemoveAt(deliveredRecipeIndex);
        currentWaitingRecipeNumber -= 1;
        totalRecipeDelivered++;
        onRecipeCompleted?.Invoke(this, EventArgs.Empty);
    }
    [ServerRpc(RequireOwnership = false)]
    private void DeliverFailureRecipeServerRpc()
    {
        DeliverFailureRecipeClientRpc();
    }
    [ClientRpc]
    private void DeliverFailureRecipeClientRpc()
    {
        Debug.Log("Delivery Fail");
        onRecipeFailure?.Invoke(this, EventArgs.Empty);
    }
    public List<KitchenReciepeSO> GetKitchenRecipeSOList()
    {
        return waitingRecipeSOList;
    }
    public int GetTotalDeliverdRecipe()
    {
        return totalRecipeDelivered;
    }
}

