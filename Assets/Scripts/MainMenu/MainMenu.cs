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
        SceneManager.LoadScene(1);
    }
    private void QuitGame()
    {
        Application.Quit();
    }

}
