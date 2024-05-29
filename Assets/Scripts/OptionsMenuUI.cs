using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
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
    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRigt,
        Interact,
        AlterInteract,
        Escape
    }
    private void Awake()
    {
        gameObject.SetActive(false);
        buttonSoundEffectsVol.onClick.AddListener(SoundEffectVol);
        buttonMusicVol.onClick.AddListener(MusicVol);
        buttonClose.onClick.AddListener(CloseUI);
        buttonMoveUp.onClick.AddListener(() =>
        {
            ChangeKeyBinding(Binding.MoveUp);
        });
        buttonMoveDown.onClick.AddListener(() =>
        {
            ChangeKeyBinding(Binding.MoveDown);
        });
        buttonMoveLeft.onClick.AddListener(() =>
        {
            ChangeKeyBinding(Binding.MoveLeft);
        });
        buttonMoveRight.onClick.AddListener(() =>
        {
            ChangeKeyBinding(Binding.MoveRigt);
        });
        buttonInteract.onClick.AddListener(() =>
        {
            ChangeKeyBinding(Binding.Interact);
        });
        buttonAlterInteract.onClick.AddListener(() =>
        {
            ChangeKeyBinding(Binding.AlterInteract);
        });
        buttonEscape.onClick.AddListener(() =>
        {
            ChangeKeyBinding(Binding.Escape);
        });

    }
    void Start()
    {
        UpdateVisual();
    }

    public void ChangeKeyBinding(Binding binding)
    {

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
}
