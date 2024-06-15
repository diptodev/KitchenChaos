using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Generate : NetworkBehaviour
{
    private NetworkVariable<float> decreament = new NetworkVariable<float>(10);
    // Update is called once per frame
    void Update()
    {
        decreament.Value -= Time.deltaTime;
    }
}
