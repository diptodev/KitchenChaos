using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
    }

    private void StoveCounter_OnStoveStateChanged(object sender, StoveCounter.StoveState e)
    {

        if (e.stoveState == StoveCounter.State.Frying || e.stoveState == StoveCounter.State.Burning)
        {
            audioSource.Play();

        }
        else
        {
            audioSource.Pause();

        }
    }

}
