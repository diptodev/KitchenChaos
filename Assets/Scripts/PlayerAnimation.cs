using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimation : NetworkBehaviour
{
    [SerializeField] Player player;
    private Animator animator;
    private const string IS_WALKING = "isWalk";
    private float footStepTimer;
    private float footStepTimerMax = 0.1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        animator.SetBool(IS_WALKING, player.IsWalking);
        footStepTimer -= Time.deltaTime;
        if (footStepTimer < 0)
        {
            footStepTimer = footStepTimerMax;
            if (player.IsWalking)
            {
                SoundManager.instance.PlayMusicForPlayer(player.transform.position);
            }

        }
    }
}
