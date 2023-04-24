using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IsFlashing = "IsFlashing";
    
    [SerializeField] private StoveCounter _stoveCounter;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        
        _animator.SetBool(IsFlashing, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = _stoveCounter.IsFried() && e.ProgressNormalized >= burnShowProgressAmount;
        
        _animator.SetBool(IsFlashing, show);
    }
}
