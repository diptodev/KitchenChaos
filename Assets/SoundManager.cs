using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]private SoundListSO audioClipSO;
    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        DeliveryCounter.OnDeliverFailure += DeliveryCounter_OnDeliverFailure;
        DeliveryCounter.OnDeliverSuccess += DeliveryCounter_OnDeliverSuccess;
        TrashCounter.OnItemTrashed += TrashCounter_OnItemTrashed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        BaseCounter.OnPickedUpSomething += BaseCounter_OnPickedUpSomething;
        BaseCounter.OnDropedSomething += BaseCounter_OnDropedSomething;
        CompletePlateVisual.OnPickUpSomethingOnPlate += CompletePlateVisual_OnPickUpSomethingOnPlate;
        
    }

 

    private void CompletePlateVisual_OnPickUpSomethingOnPlate(object sender, System.EventArgs e)
    {
        CompletePlateVisual completePlateVisual = (CompletePlateVisual)sender;
        PlayMusic(audioClipSO.objectDrop,completePlateVisual.transform.position );
    }

    private void BaseCounter_OnDropedSomething(object sender, System.EventArgs e)
    {
        PlayMusic(audioClipSO.objectDrop, Player.playerInstance.transform.position);
    }

    private void BaseCounter_OnPickedUpSomething(object sender, System.EventArgs e)
    {
        PlayMusic(audioClipSO.objectPickup, Player.playerInstance.transform.position);
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

    private void PlayMusic(AudioClip[] audioClipArray, Vector3 position,float vol=1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0,audioClipArray.Length)],position,vol);
    }
    public void PlayMusicForPlayer(Vector3 position)
    {
        PlayMusic(audioClipSO.footStep, position);
    }
}