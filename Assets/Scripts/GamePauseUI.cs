using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button mainMenu;
    [SerializeField] private Button buttonOptions;
    [SerializeField] private GameObject optionsMenu;
    private bool gamePauseStatus;
    private bool isOptionsLoaded;
    private void Awake()
    {
        buttonResume.onClick.AddListener(ResumeGame);
        mainMenu.onClick.AddListener(LoadMainmenu);
        buttonOptions.onClick.AddListener(LoadOptions);
        isOptionsLoaded = false;
    }
    private void Start()
    {
        GameInput.onGamePauseEvent += GamepauseEventInvoked;
        gamePauseStatus = false;
        transform.gameObject.SetActive(gamePauseStatus);
    }
    private void GamepauseEventInvoked(object sender, EventArgs args)
    {
        ToggleGameStatus();

    }
    private void ResumeGame()
    {
        ToggleGameStatus();
    }
    private void ToggleGameStatus()
    {
        gamePauseStatus = !gamePauseStatus;
        transform.gameObject.SetActive(gamePauseStatus);
        GameManager.Instance.TooglePauseGame();
    }
    private void LoadMainmenu()
    {
        SceneLoader.LoadNewScene(SceneLoader.SceneState.MainMenuScene);
    }
    private void LoadOptions()
    {
        isOptionsLoaded = !isOptionsLoaded;
        optionsMenu.SetActive(isOptionsLoaded);
    }
}
