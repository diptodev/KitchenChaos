using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonQuit;

    private void Awake()
    {
        buttonPlay.onClick.AddListener(PlayGame);
        buttonPlay.onClick.AddListener(QuitGame);
    }
    private void PlayGame()
    {
        SceneLoader.LoadNewScene(SceneLoader.SceneState.LoadingScene);
    }
    private void QuitGame()
    {
        Application.Quit();
    }

}
