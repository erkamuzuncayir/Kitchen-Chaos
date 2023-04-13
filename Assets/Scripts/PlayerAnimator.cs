using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimator : MonoBehaviour
{
    private const string _IS_WALKING = "IsWalking";
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Player player;

    private void Update()
    {
        playerAnimator.SetBool(_IS_WALKING, player.IsWalking());
    }
}
