using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string _OPEN_CLOSE = "OpenClose";
    [SerializeField] private ContainerCounter containerCounter;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObjects += ContainerCounter_OnPlayerGrabbedObjects;
    }

    private void ContainerCounter_OnPlayerGrabbedObjects(object sender, EventArgs e)
    {
        _animator.SetTrigger(_OPEN_CLOSE);
    }
}
