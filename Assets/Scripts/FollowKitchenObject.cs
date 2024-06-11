using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FollowKitchenObject : NetworkBehaviour
{
    private Transform targetFollowTransform;
    public void SetTargetKitchenTransform(Transform targetFollowTransform)
    {
        this.targetFollowTransform = targetFollowTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetFollowTransform == null)
        {
            return;
        }
        transform.position = targetFollowTransform.position;
        transform.rotation = targetFollowTransform.rotation;

    }
}
