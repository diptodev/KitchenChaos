using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundListSO audioClipSO;
    [SerializeField] private DeliveryCounter deliveryCounter;
    [SerializeField] private DeliveryCounter deliveryCounter1;
    private const string SOUND_MANAGER_PLAYER_PREFS = "SoundManagerPlayerRefs";
    private float volume;
    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        deliveryCounter.OnDeliverFailure += DeliveryCounter_OnDeliverFailure;
        deliveryCounter.OnDeliverSuccess += DeliveryCounter_OnDeliverSuccess;
        deliveryCounter1.OnDeliverFailure += DeliveryCounter_OnDeliverFailure;
        deliveryCounter1.OnDeliverSuccess += DeliveryCounter_OnDeliverSuccess;
        TrashCounter.OnItemTrashed += TrashCounter_OnItemTrashed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        BaseCounter.OnPickedUpSomething += BaseCounter_OnPickedUpSomething;
        BaseCounter.OnDropedSomething += BaseCounter_OnDropedSomething;
        CompletePlateVisual.OnPickUpSomethingOnPlate += CompletePlateVisual_OnPickUpSomethingOnPlate;
        volume = PlayerPrefs.GetFloat(SOUND_MANAGER_PLAYER_PREFS, 1f);
    }



    private void CompletePlateVisual_OnPickUpSomethingOnPlate(object sender, System.EventArgs e)
    {
        CompletePlateVisual completePlateVisual = (CompletePlateVisual)sender;
        PlayMusic(audioClipSO.objectDrop, completePlateVisual.transform.position);
    }

    private void BaseCounter_OnDropedSomething(object sender, System.EventArgs e)
    {
        //  PlayMusic(audioClipSO.objectDrop, Player.playerInstance.transform.position);
    }

    private void BaseCounter_OnPickedUpSomething(object sender, System.EventArgs e)
    {
        // PlayMusic(audioClipSO.objectPickup, Player.playerInstance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlayMusic(audioClipSO.chop, cuttingCounter.transform.position);
    }

    private void TrashCounter_OnItemTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlayMusic(audioClipSO.trash, trashCounter.transform.position);
    }

    private void DeliveryCounter_OnDeliverSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = (DeliveryCounter)sender;
        PlayMusic(audioClipSO.deliverSuccess, deliveryCounter.transform.position);
    }

    private void DeliveryCounter_OnDeliverFailure(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = (DeliveryCounter)sender;
        PlayMusic(audioClipSO.deliverFailure, deliveryCounter.transform.position);
    }

    private void PlayMusic(AudioClip[] audioClipArray, Vector3 position, float vol = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, vol * volume);
    }
    public void PlayMusicForPlayer(Vector3 position)
    {
        PlayMusic(audioClipSO.footStep, position);
    }
    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume > 1)
        {
            volume = 0;
        }
        PlayerPrefs.SetFloat(SOUND_MANAGER_PLAYER_PREFS, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume;
    }
}
