using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] private Button buttonPlayerReady;
    [SerializeField] private Button buttonMainmenu;
    private void Awake()
    {
        buttonPlayerReady.onClick.AddListener(() =>
        {
            CharacterSelectReady.instance.SetPlayerReady();

        });
        buttonMainmenu.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            SceneLoader.LoadNewScene(SceneLoader.SceneState.MainMenuScene);
        });
    }

}
