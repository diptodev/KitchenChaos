using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingCharacterSelectUI : MonoBehaviour
{
    [SerializeField] private Button buttonReady;
    private void Awake()
    {
        buttonReady.onClick.AddListener(() =>
        {
            CharacterSelectReady.instance.SetPlayerReady();
        });
    }
}
