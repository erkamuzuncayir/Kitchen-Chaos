using System;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _keyMoveUpText;
    [SerializeField] private TextMeshProUGUI _keyMoveDownText;
    [SerializeField] private TextMeshProUGUI _keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI _keyMoveRightText;
    [SerializeField] private TextMeshProUGUI _keyInteractText;
    [SerializeField] private TextMeshProUGUI _keyInteractAlternateText;
    [SerializeField] private TextMeshProUGUI _keyPauseText;
    [SerializeField] private TextMeshProUGUI _keyGamepadInteractText;
    [SerializeField] private TextMeshProUGUI _keyGamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI _keyGamepadPauseText;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        UpdateVisual();
        
        Show();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        _keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
        _keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
        _keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
        _keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
        _keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        _keyInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        _keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        _keyGamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteract);
        _keyGamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadInteractAlternate);
        _keyGamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamepadPause);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
