using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter == clearCounter)
        {
            ShowVisual();
        }
        else
        {
            HideVisual();
        }
    }

    private void ShowVisual()
    {
        visualGameObject.SetActive(true);
    }
    
    private void HideVisual()
    {
        visualGameObject.SetActive(false);
    }
}
