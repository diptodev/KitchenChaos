using System;
using Unity.Netcode;
using UnityEngine;

public class StoveCounter : BaseCounter, IProgressBarUI
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private GameObject fireEffect;
    public event EventHandler<StoveState> OnStoveStateChanged;
    public class StoveState : EventArgs
    {
        public State stoveState;
    }
    private KitchenObjectSO kitchenObjectSO;
    FryingRecipeSO fryingRecipeSO;
    private NetworkVariable<float> cookingTime = new NetworkVariable<float>(0f);

    private NetworkVariable<float> restingTime = new NetworkVariable<float>(0f);
    private NetworkVariable<float> burningTime = new NetworkVariable<float>(0f);

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Resting,
        Burning,
        Burned,
        Default
    }
    private NetworkVariable<State> state = new NetworkVariable<State>(State.Idle);
    public override void OnNetworkSpawn()
    {
        cookingTime.OnValueChanged += CookingTimerOnValueChanged;
        restingTime.OnValueChanged += RestingTimerOnValueChanged;
        burningTime.OnValueChanged += BurningTimerOnValueChanged;
        state.OnValueChanged += StateOnValueChanged;
    }
    public event EventHandler<IProgressBarUI.OnIProgressBarUIEventArgs> OnIProgressBarUI;
    public class OnIProgressBarUIEventArgs : EventArgs
    {
        public float normalizedProgressBarValue;
        public string modeColor;
    }
    private void StateOnValueChanged(State preState, State newState)
    {
        OnStoveStateChanged?.Invoke(this, new StoveState
        {
            stoveState = newState
        });
    }
    private void CookingTimerOnValueChanged(float prevValue, float newValue)
    {
        float fryingTimerMax = fryingRecipeSO != null ? fryingRecipeSO.maxCookedTime : 1f;
        OnIProgressBarUI.Invoke(this, new IProgressBarUI.OnIProgressBarUIEventArgs
        {
            normalizedProgressBarValue = cookingTime.Value / fryingTimerMax,
            modeColor = "burning"
        });
    }
    private void RestingTimerOnValueChanged(float prevValue, float newValue)
    {
        float restingTimerMax = fryingRecipeSO != null ? fryingRecipeSO.maxRestingTime : 1f;
        OnIProgressBarUI.Invoke(this, new IProgressBarUI.OnIProgressBarUIEventArgs
        {
            normalizedProgressBarValue = restingTime.Value / restingTimerMax,
            modeColor = "resting"
        });
    }
    private void BurningTimerOnValueChanged(float prevValue, float newValue)
    {
        float burningTimerMax = fryingRecipeSO != null ? fryingRecipeSO.maxBurningTime : 1f;
        OnIProgressBarUI.Invoke(this, new IProgressBarUI.OnIProgressBarUIEventArgs
        {
            normalizedProgressBarValue = burningTime.Value / burningTimerMax,
            modeColor = "black"
        });
    }
    private void Update()
    {
        if (!IsServer) return;
        if (kitchenObjectSO != null)
        {
            switch (state.Value)
            {
                case State.Idle:
                    state.Value = State.Frying;
                    break;
                case State.Frying:

                    if (cookingTime.Value >= fryingRecipeSO.maxCookedTime)
                    {
                        state.Value = State.Fried;

                    }
                    else { cookingTime.Value += Time.deltaTime; }

                    break;
                case State.Fried:

                    KitchenObject.DestroyKitchenObjectFromServer(GetKitchenObject());
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.cooked, this);
                    state.Value = State.Resting;

                    break;
                case State.Resting:


                    if (restingTime.Value >= fryingRecipeSO.maxRestingTime)
                    {
                        state.Value = State.Burning;

                    }
                    else
                    {
                        restingTime.Value += Time.deltaTime;

                    }
                    break;
                case State.Burning:

                    if (burningTime.Value >= fryingRecipeSO.maxBurningTime)
                    {

                        state.Value = State.Burned;
                        KitchenObject.DestroyKitchenObjectFromServer(GetKitchenObject());
                    }
                    else
                    {
                        burningTime.Value += Time.deltaTime;

                    }
                    break;
                case State.Burned:

                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.burned, this);
                    ResetStoveClientRpc();
                    state.Value = State.Default;
                    break;
                case State.Default:
                    ;
                    // Debug.Log("Please put it on the trash");

                    break;
            }
        }

    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //ClearCounter has nothing

            if (player.HasKitchenObject())
            {
                //Player has something and give it to the clearcounter
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    KitchenObject kitchenObject = player.GetKitchenObject();
                    kitchenObject.SetIKitchenObjectParent(this);
                    int kitchenObjectSOIndex = KitchenGameMultiplayer.Instance.GetKicthenOBjectIndexFromKitchenObjectSO(kitchenObject.GetKitchenObjectSO());
                    InteractLogicServerRpc(kitchenObjectSOIndex);
                }
                else
                {
                    kitchenObjectSO = null;
                }
            }
            else
            {

                //Player has Nothing
            }
        }

        else
        {
            //ClearCounter Has SomeThing
            if (!player.HasKitchenObject())
            {
                //ClearCounter has something and give it to the player
                if (state.Value == State.Burned || state.Value == State.Default)
                {
                    GetKitchenObject().SetIKitchenObjectParent(player);
                    ResetStoveServerRpc();
                }
            }
            else
            {
                //ClearCounter has something but player has also something
                if (state.Value == State.Fried || state.Value == State.Resting || state.Value == State.Burning || state.Value == State.Default || state.Value == State.Burned)
                {
                    if (player.GetKitchenObject().TryKitchenPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            KitchenObject.DestroyKitchenObjectFromServer(GetKitchenObject());
                            ResetStoveServerRpc();
                        }
                    }
                }
            }

        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractLogicServerRpc(int kitchenObjectSOIndex)
    {
        cookingTime.Value = 0;
        restingTime.Value = 0;
        burningTime.Value = 0;
        state.Value = State.Idle;
        InteractLogicClientRpc(kitchenObjectSOIndex);
    }
    [ClientRpc]
    private void InteractLogicClientRpc(int kitchenObjectSOIndex)
    {
        kitchenObjectSO = KitchenGameMultiplayer.Instance.GetKitchenObjectSOFromIndex(kitchenObjectSOIndex);
        fryingRecipeSO = GetSelectedFryingRecipe(kitchenObjectSO);
        fireEffect.SetActive(true);
        OnIProgressBarUI.Invoke(this, new IProgressBarUI.OnIProgressBarUIEventArgs
        {
            normalizedProgressBarValue = 0,
            modeColor = "burning"
        });

        OnStoveStateChanged?.Invoke(this, new StoveState
        {
            stoveState = State.Idle
        });
    }
    [ServerRpc(RequireOwnership = false)]
    private void ResetStoveServerRpc()
    {

        kitchenObjectSO = null;
        cookingTime.Value = 0;
        restingTime.Value = 0;
        burningTime.Value = 0;
        state.Value = State.Idle;
        ResetStoveClientRpc();
    }
    [ClientRpc]
    private void ResetStoveClientRpc()
    {
        fireEffect.SetActive(false);
        OnIProgressBarUI.Invoke(this, new IProgressBarUI.OnIProgressBarUIEventArgs
        {
            normalizedProgressBarValue = 0,
            modeColor = "burning"
        });
        OnStoveStateChanged?.Invoke(this, new StoveState
        {
            stoveState = State.Idle
        });
    }
    public override void AlternateInteract()
    {
        /* if (kitchenObjectSO != null)
         {

             if (HasKitchenObject() && HasRecipeWithInput(kitchenObjectSO))
             {
                 KitchenObjectSO outputKitchenObject = GetKitchenObjectSO(kitchenObjectSO);
                 GetKitchenObject().DestroyKitchenObject();
                 KitchenObject.SpawnKitchenObject(outputKitchenObject, this);

             }

         }*/
    }
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (FryingRecipeSO cuttingRecipeSO in fryingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == kitchenObjectSO) return true;
        }
        return false;
    }
    private KitchenObjectSO GetKitchenObjectSO(KitchenObjectSO inputKitchenObject)
    {
        return GetSelectedFryingRecipe(inputKitchenObject).cooked;
    }

    private FryingRecipeSO GetSelectedFryingRecipe(KitchenObjectSO inputKitchenObject)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObject) return fryingRecipeSO;
        }
        return null;
    }
}
