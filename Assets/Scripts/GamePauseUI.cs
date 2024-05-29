using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button mainMenu;
    private bool gamePauseStatus;
private void Awake() {
    buttonResume.onClick.AddListener(ResumeGame);
    mainMenu.onClick.AddListener(LoadMainmenu);
}
private void Start() {
    GameInput.onGamePauseEvent+=GamepauseEventInvoked;
    gamePauseStatus=false;
    transform.gameObject.SetActive(gamePauseStatus);
}
private void GamepauseEventInvoked(object sender,EventArgs args){
ToggleGameStatus();

}
private void ResumeGame(){
ToggleGameStatus();
}
private void ToggleGameStatus(){
 gamePauseStatus=!gamePauseStatus;
 transform.gameObject.SetActive(gamePauseStatus);
 GameManager.Instance.TooglePauseGame();
}
private void LoadMainmenu(){
SceneLoader.LoadNewScene(SceneLoader.SceneState.MainMenuScene);
}
    // Update is called once per frame
    
}
