using System;
using UnityEngine;

public class BakeCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler<OnBakingEventsArgs> OnBaking;
    public class OnBakingEventsArgs : EventArgs
    {
        public bool isBaking;
    }
    [SerializeField] private GameObject microLight;
    private KitchenObjects kitchenObject;

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
                OnBaking?.Invoke(this, new OnBakingEventsArgs
                {
                    isBaking = false
                });
                break;

            case State.Baking:
                OnBaking?.Invoke(this, new OnBakingEventsArgs
                {
                    isBaking = true
                });
                break;

            case State.Burning:
                OnBaking?.Invoke(this, new OnBakingEventsArgs
                {
                    isBaking = true
                });
                break;

            case State.Burned:
                OnBaking?.Invoke(this, new OnBakingEventsArgs
                {
                    isBaking = false
                });
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

    public KitchenObjects SetKitchenObjectParent()
    {
        throw new System.NotImplementedException();
    }

    public KitchenObjects SpawnKitchenObject()
    {
        throw new System.NotImplementedException();
    }

    public Transform GetObjectFollowTransform()
    {
        return counterTopPosition;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public KitchenObjects GetKitchenObjects()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObjects kitchenObj)
    {
        this.kitchenObject = kitchenObj;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
}
