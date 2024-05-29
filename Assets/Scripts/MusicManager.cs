using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance { get; private set; }
    private const string MUSIC_MANAGER_PLAYER_PREFS = "MusicManagerPlayerPrefs";
    private float volume;
    private AudioSource audioSource;
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(MUSIC_MANAGER_PLAYER_PREFS, 0.3f);
    }
    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume > 1)
        {
            volume = 0;
        }
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_MANAGER_PLAYER_PREFS, volume);
        PlayerPrefs.Save();
    }
    public float GetMusicVolume()
    {
        return volume;
    }

}
