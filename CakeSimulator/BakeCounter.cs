using System;
using UnityEngine;
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
    private float bakeTimer;
    private float burnTimer;

    public enum State
    {
        Idle,
        Baking,
        Burning,
        Burned

    }

    private State state;


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
                OnMicrowaveStateChanged?.Invoke(this, new OnMicrowaveStateChangedEventArgs
                {
                    state = state
                });
                bakeTimer += Time.deltaTime;
                float bakeTimerMax = bakingRecipeSO.bakeTimer;
                if(bakeTimer < bakeTimerMax)
                {
                    float bakeProgress = bakeTimer / bakeTimerMax;
                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormaliazed = bakeProgress
                    });

                }
                
                break;

            case State.Burning:
                OnMicrowaveStateChanged?.Invoke(this, new OnMicrowaveStateChangedEventArgs
                {
                    state = state
                });
                burnTimer += Time.deltaTime;
                float burnTimerMax = bakingRecipeSO.bakeTimer;
                if (burnTimer < burnTimerMax)
                {
                    float bakeProgress = burnTimer / burnTimerMax;
                    OnProgressChanged?.Invoke(this, new IProgressBar.OnProgressChangedEventArgs
                    {
                        progressNormaliazed = bakeProgress
                    });

                }
                break;

            case State.Burned:
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
                //Give object to player
                GetKitchenObjects().SetKitchenObjectParent(player);
                state  = State.Idle;
               
            }

        }
        else
        {
            if (player.HasKitchenObject())
            {
                //Place object on the counter
                player.GetKitchenObjects().SetKitchenObjectParent(this);
                state = State.Baking;
               
            }
            else
            {
                //Do nothing
            }

        }

    }

   
}
