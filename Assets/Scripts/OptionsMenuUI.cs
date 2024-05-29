using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject keyRebindUI;
    [SerializeField] private Button buttonSoundEffectsVol;
    [SerializeField] private Button buttonMusicVol;
    [SerializeField] private Button buttonClose;
    [SerializeField] private Button buttonMoveUp;
    [SerializeField] private Button buttonMoveDown;
    [SerializeField] private Button buttonMoveLeft;
    [SerializeField] private Button buttonMoveRight;
    [SerializeField] private Button buttonInteract;
    [SerializeField] private Button buttonAlterInteract;
    [SerializeField] private Button buttonEscape;
    [SerializeField] private Text txtsoundEffectsVol;
    [SerializeField] private Text txtMusicVol;
    [SerializeField] private Text txtMoveUp;
    [SerializeField] private Text txtMoveDown;
    [SerializeField] private Text txtMoveLeft;
    [SerializeField] private Text txtMoveRight;
    [SerializeField] private Text txtInteract;
    [SerializeField] private Text txtAlternativeInteract;
    [SerializeField] private Text txtEscape;
    private void Awake()
    {
        gameObject.SetActive(false);
        buttonSoundEffectsVol.onClick.AddListener(SoundEffectVol);
        buttonMusicVol.onClick.AddListener(MusicVol);
        buttonClose.onClick.AddListener(CloseUI);
        buttonMoveUp.onClick.AddListener(() =>
        {
            ChangeKeyBinding(GameInput.Binding.MoveUp);
        });
        buttonMoveDown.onClick.AddListener(() =>
        {
            ChangeKeyBinding(GameInput.Binding.MoveDown);
        });
        buttonMoveLeft.onClick.AddListener(() =>
        {
            ChangeKeyBinding(GameInput.Binding.MoveLeft);
        });
        buttonMoveRight.onClick.AddListener(() =>
        {
            ChangeKeyBinding(GameInput.Binding.MoveRigt);
        });
        buttonInteract.onClick.AddListener(() =>
        {
            ChangeKeyBinding(GameInput.Binding.Interact);
        });
        buttonAlterInteract.onClick.AddListener(() =>
        {
            ChangeKeyBinding(GameInput.Binding.AlterInteract);
        });
        buttonEscape.onClick.AddListener(() =>
        {
            ChangeKeyBinding(GameInput.Binding.Escape);
        });

    }
    void Start()
    {
        UpdateVisual();
        HideRebindingUI();
    }

    public void ChangeKeyBinding(GameInput.Binding binding)
    {
        ShowRebindingUI();
        GameInput.instance.ChangeKeyBinding(binding, () =>
        {
            HideRebindingUI();
            UpdateVisual();
        });

    }
    private void SoundEffectVol()
    {
        SoundManager.instance.ChangeVolume();
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        txtsoundEffectsVol.text = "Sound Effects Vol : " + Math.Ceiling(SoundManager.instance.GetVolume() * 10).ToString();
        txtMusicVol.text = "Music Vol : " + Math.Ceiling(MusicManager.instance.GetMusicVolume() * 10).ToString();
        txtMoveUp.text = GameInput.instance.GetKeyBinding(GameInput.Binding.MoveUp);
        txtMoveDown.text = GameInput.instance.GetKeyBinding(GameInput.Binding.MoveDown);
        txtMoveLeft.text = GameInput.instance.GetKeyBinding(GameInput.Binding.MoveLeft);
        txtMoveRight.text = GameInput.instance.GetKeyBinding(GameInput.Binding.MoveRigt);
        txtInteract.text = GameInput.instance.GetKeyBinding(GameInput.Binding.Interact);
        txtAlternativeInteract.text = GameInput.instance.GetKeyBinding(GameInput.Binding.AlterInteract);
        txtEscape.text = GameInput.instance.GetKeyBinding(GameInput.Binding.Escape);
    }
    private void MusicVol()
    {
        MusicManager.instance.ChangeVolume();
        UpdateVisual();
    }
    private void CloseUI()
    {
        gameObject.SetActive(false);
    }
    private void HideRebindingUI()
    {
        keyRebindUI.SetActive(false);
    }
    private void ShowRebindingUI()
    {
        keyRebindUI.SetActive(true);
    }
}
