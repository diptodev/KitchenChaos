using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public static class SceneLoader
{
  public enum SceneState
  {
    GameScene,
    MainMenuScene,
    LoadingScene
  }
  public static void LoadNewScene(SceneState sceneState)
  {
    SceneManager.LoadScene(sceneState.ToString());
  }
  public static void LoaderCallback()
  {
    LoadNewScene(SceneState.GameScene);
  }
}
