using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private GameObject fireEffect;
    private KitchenObjectSO kitchenObjectSO;
    private float cookingTime;
    private float restingTime;
    private float burningTime;
    private enum State
    {
        Idle,
        Frying,
        Fried,
        Resting,
        Burning,
        Burned,
        Default
    }
    private State state;
    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        Debug.Log(state);
        if (kitchenObjectSO != null)
        {

            FryingRecipeSO frying = GetSelectedFryingRecipe(kitchenObjectSO);


            switch (state)
            {
                case State.Idle:
                    state = State.Frying;
                    break;
                case State.Frying:

                    if (cookingTime >= frying.maxCookedTime)
                    {
                        state = State.Fried;
                    }
                    else cookingTime += Time.deltaTime;
                    break;
                case State.Fried:
                    GetKitchenObject().DestroyKitchenObject();
                    KitchenObject.SpawnKitchenObject(frying.cooked, this);
                    state = State.Resting;
                    break;
                case State.Resting:
                    if (restingTime > 3)
                    {
                        state = State.Burning;
                    }
                    else
                    {
                        restingTime += Time.deltaTime;

                    }
                    break;
                case State.Burning:

                    if (burningTime > 3)
                    {
                        state = State.Burned;
                        GetKitchenObject().DestroyKitchenObject();
                    }
                    else
                    {
                        burningTime += Time.deltaTime;

                    }
                    break;
                case State.Burned:
                    KitchenObject.SpawnKitchenObject(frying.burned, this);
                    state = State.Default;

                    break;
                case State.Default:
                    fireEffect.SetActive(false);
                    Debug.Log("Please put it on the trash");
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
                    player.GetKitchenObject().SetIKitchenObjectParent(this);
                    kitchenObjectSO = GetKitchenObject().GetKitchenObjectSO();
                    cookingTime = 0;
                    restingTime = 0;
                    burningTime = 0;
                    state = State.Idle;
                    fireEffect.SetActive(true);
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
                GetKitchenObject().SetIKitchenObjectParent(player);
                kitchenObjectSO = null;
                cookingTime = 0;
                restingTime = 0;
                burningTime = 0;
                state = State.Idle;
                fireEffect.SetActive(false);
            }
            else
            {
                //ClearCounter has something but player has also something
            }
        }
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
