using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Player player;
    private Animator animator;
    private const string IS_WALKING= "isWalk";
    private void Awake()
    { 
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking);
    }
}
