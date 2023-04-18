using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State State;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State _state;
    private float _fryingTimer;
    private FryingRecipeSO _fryingRecipeSO;
    private float _burningTimer;
    private BurningRecipeSO _burningRecipeSO;



    private void Start()
    {
        _state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (_state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    _fryingTimer += Time.deltaTime;
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = _fryingTimer / _fryingRecipeSO.fryingTimerMax
                    });
                    
                    if (_fryingTimer > _fryingRecipeSO.fryingTimerMax)
                    {
                        // Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(_fryingRecipeSO.output, this);

                        Debug.Log("Object fried!");

                        _state = State.Fried;
                        _burningTimer = 0f;
                        _burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            State = _state
                        });
                    }
                    break;
                case State.Fried:
                    _burningTimer += Time.deltaTime;
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = _burningTimer / _burningRecipeSO.burningTimerMax
                    });
                    
                    if (_burningTimer > _burningRecipeSO.burningTimerMax)
                    {
                        // Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(_burningRecipeSO.output, this);
                        
                        _state = State.Burned;
                        
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            State = _state
                        });
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            ProgressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    // Player carrying something that can be Fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                _fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                _state = State.Frying;
                _fryingTimer = 0f;
                
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    State = _state
                });
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    ProgressNormalized = _fryingTimer / _fryingRecipeSO.fryingTimerMax
                });
            }
        }
        else
        {
            // There is a KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);

                _state = State.Idle;
                
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    State = _state
                });
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    ProgressNormalized = 0f
                });
            }
        }
    }
    
    
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
            return fryingRecipeSO.output;
        else
            return null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

}