using System;
using UnityEngine;
using static IProgressBar;

public class StoveCounter : BaseCounter, IProgressBar
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    [SerializeField] private FryingReciepeSO[] receipeObjects;
    public event EventHandler<OnStoveStateChangedEventAgrs> OnStoveStateChanged;
    public class OnStoveStateChangedEventAgrs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingReciepeSO fryingReciepeSO;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenobject())
        {
            switch (state)
            {
                case State.Idle:
                    break;

                case State.Frying:
                    fryingTimer += Time.deltaTime;
                  
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormaliazed = fryingTimer / fryingReciepeSO.fryingTimerCount
                    });

                    if (fryingTimer > fryingReciepeSO.fryingTimerCount)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObjects.SpawnKitchenObject(fryingReciepeSO.cookedMeatPatty, this);

                        Debug.Log("Fried");
                        burningTimer = 0f;

                        state = State.Fried;
                        OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventAgrs
                        {
                            state = state
                        });

                    }
                    break;

                case State.Fried:
                    burningTimer += Time.deltaTime;
                   
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormaliazed = burningTimer / fryingReciepeSO.burnedTimerCount
                    });
                    if (burningTimer > fryingReciepeSO.burnedTimerCount)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObjects.SpawnKitchenObject(fryingReciepeSO.burnedMeatPatty, this);

                        Debug.Log("OverCooked");
                        state = State.Burned;

                        OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventAgrs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                        {
                            progressNormaliazed = 0f
                        });

                    }
                    break;

                case State.Burned:
                    break;
              
            }
        }
    }
    public override void Interact(PlayerControl player)
    {
        if (!HasKitchenobject())
        {
            if (player.HasKitchenobject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingReciepeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    fryingTimer = 0f;
                    state = State.Frying;


                    OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventAgrs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormaliazed = fryingTimer / fryingReciepeSO.fryingTimerCount
                    });
                }
            }
        }
        else
        {
            //Do nothing counter already has an object
            if (!player.HasKitchenobject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
                OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventAgrs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    progressNormaliazed = 0f
                });
            }

        }
    }


    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectsSO)
    {
        FryingReciepeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectsSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectsSO GetOutputForInputSO(KitchenObjectsSO inputKitchenObjectSO)
    {
        FryingReciepeSO outputKitchenObjectSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (outputKitchenObjectSO != null)
        {
            return outputKitchenObjectSO.cookedMeatPatty;
        }
        else
        {
            return null;
        }
    }

    private FryingReciepeSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (FryingReciepeSO cuttingRecipe in receipeObjects)
        {
            if (cuttingRecipe.unCookedMeatPatty == inputKitchenObjectSO)
            {
                return cuttingRecipe;
            }
        }
        return null;
    }

}
