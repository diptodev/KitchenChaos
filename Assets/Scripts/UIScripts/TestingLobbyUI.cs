using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingLobbyUI : MonoBehaviour
{
    [SerializeField] private Button buttonCreateGame;
    [SerializeField] private Button buttonJoinGame;
    private void Awake()
    {
        buttonCreateGame.onClick.AddListener(() =>
        {
            KitchenGameMultiplayer.Instance.StartHost();
            SceneLoader.LoadSceneFromNetwork(SceneLoader.SceneState.CharacterSelectScene);
        });
        buttonJoinGame.onClick.AddListener(() =>
        {
            KitchenGameMultiplayer.Instance.StartCLient();
        });
    }
}
