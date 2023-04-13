using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string _IS_WALKING = "IsWalking";
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Player player;

    private void Update()
    {
        _playerAnimator.SetBool(_IS_WALKING, player.IsWalking());
    }
}
