using System;
using UnityEngine;
using static IProgressBar;
using static PlateKitchenObject_Visual;
//using static IProgressBar;

public class BakeCounter : BaseCounter, IProgressBar
{
    public event EventHandler<IProgressBar.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnMicrowaveStateChangedEventArgs> OnMicrowaveStateChanged;
 
    public class OnMicrowaveStateChangedEventArgs : EventArgs
    {
        public State state;
    }
 
    [SerializeField] private BakingRecipeSO bakingRecipeSO;
    //[SerializeField] private PlateKitchenObject_Visual plateKitchenObject_Visual;
    private float bakeTimer;
    private float burnTimer;


    public enum State
    {
        Idle,
        Baking,
        Burning,
        Burned

    }

    public State state;


    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        switch (state) {
        
            case State.Idle:
                break;

            case State.Baking:
                Debug.Log("Baking state");

                bakeTimer += Time.deltaTime;
                float bakeTimerMax = bakingRecipeSO.bakeTimer;
                float bakeProgress = bakeTimer / bakeTimerMax;
                OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                {
                    progressNormaliazed = bakeProgress
                });
                if (bakeTimer > bakeTimerMax)
                {
                    if (GetKitchenObjects().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngridient(bakingRecipeSO.bakedCakeBase))
                        {
                            //Debug.Log(0);

                            var visual = plateKitchenObject.transform.Find("CakeModel").GetComponent<PlateKitchenObject_Visual>();

                            if (visual == null)
                            {
                                Debug.LogError("Visual is NULL!");
                                return;
                            }

                            foreach (var kitchenObjectsSOGameObject in visual.GetKitchenObjectSOGameObjectList())
                            {
                                //Debug.Log(1);
                                if(bakingRecipeSO.unBakedCakeBase == kitchenObjectsSOGameObject.kitchenObjectsSO)
                                {
                                    plateKitchenObject.GetKitchenObjectsSOList().Remove(bakingRecipeSO.unBakedCakeBase);
                                    kitchenObjectsSOGameObject.gameObject.SetActive(false);
                                    //Debug.Log(kitchenObjectsSOGameObject.gameObject);
                                    break;
                                }
                            }
                        }
                    }
                    //GetKitchenObjects().DestroySelf();
                    //KitchenObjects.SpawnKitchenObject(bakingRecipeSO.bakedCakeBase, this);
                    burnTimer = 0f;
                    state = State.Burning;
                    OnMicrowaveStateChanged?.Invoke(this, new OnMicrowaveStateChangedEventArgs
                    {
                        state = state
                    });

                }
               
                    break;

            case State.Burning:
                Debug.Log("Burning state");

                burnTimer += Time.deltaTime;
                float burnTimerMax = bakingRecipeSO.burnTimer;
                bakeProgress = burnTimer / burnTimerMax;
                OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                {
                    progressNormaliazed = bakeProgress
                });

                if (burnTimer > burnTimerMax)
                {
                    state = State.Burned;
                    if (GetKitchenObjects().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngridient(bakingRecipeSO.bakedCakeBase))
                        {
                            //GetKitchenObjects().DestroySelf();
                        }
                    }
                    //GetKitchenObjects().DestroySelf();
                    //KitchenObjects.SpawnKitchenObject(bakingRecipeSO.overbakedCakeBase, this);
                    OnMicrowaveStateChanged?.Invoke(this, new OnMicrowaveStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormaliazed = 0f
                    });


                }

                break;

            case State.Burned:
                Debug.Log("Burned state");
                break;
        }
    }

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                //Don't do anything
            }
            else
            {
                //Give it to the player
            
                GetKitchenObjects().SetKitchenObjectParent(player);
                state = State.Idle;

                OnMicrowaveStateChanged?.Invoke(this, new OnMicrowaveStateChangedEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    progressNormaliazed = 0f
                });

            }

        }
        else
        {
            if (player.HasKitchenObject())
            {
                //Place object on the counter

                if (player.GetKitchenObjects().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.GetIngredientCount() <= 3)
                    {
                        plateKitchenObject.SetKitchenObjectParent(this);

                        bakeTimer = 0f;
                        state = State.Baking;
                        OnMicrowaveStateChanged?.Invoke(this, new OnMicrowaveStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                }

              
                //if (player.GetKitchenObjects().GetKitchenObjectsSO() == bakingRecipeSO.unBakedCakeBase)
                //{

                //    player.GetKitchenObjects().SetKitchenObjectParent(this);
                //    bakeTimer = 0f;
                //    state = State.Baking;
                //    OnMicrowaveStateChanged?.Invoke(this, new OnMicrowaveStateChangedEventArgs
                //    {
                //        state = state
                //    });
                //}

            }
            else
            {
                //Do nothing
                Debug.Log("Hell");
            }

        }

    }

}
