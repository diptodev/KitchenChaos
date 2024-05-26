using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class SoundListSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliverSuccess;
    public AudioClip[] deliverFailure;
    public AudioClip[] footStep;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip stroveSizzle;
    public AudioClip[] warning;
    public AudioClip[] trash;
}
