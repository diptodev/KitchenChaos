using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public static class SceneLoader
{
  public enum SceneState
  {
    GameScene,
    MainMenuScene,
    LoadingScene,
    LobbyScene,
    CharacterSelectScene
  }
  public static void LoadNewScene(SceneState sceneState)
  {
    SceneManager.LoadScene(sceneState.ToString());
  }
  public static void LoaderCallback()
  {
    LoadNewScene(SceneState.GameScene);
  }
  public static void LoadSceneFromNetwork(SceneState sceneState)
  {
    NetworkManager.Singleton.SceneManager.LoadScene(sceneState.ToString(), LoadSceneMode.Single);
  }
}
