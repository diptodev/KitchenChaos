using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private Animator animator;

    private const string IS_BURNING = "IsBurning";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveCounterStateChanged;
    }
    private void StoveCounter_OnStoveCounterStateChanged(object sender, StoveCounter.StoveState e)
    {

        if (e.stoveState == StoveCounter.State.Idle || e.stoveState == StoveCounter.State.Frying || e.stoveState == StoveCounter.State.Fried
       || e.stoveState == StoveCounter.State.Burned || e.stoveState == StoveCounter.State.Default
        )
        {
            gameObject.SetActive(false);
            animator.SetBool(IS_BURNING, false);
        }
        else
        {
            gameObject.SetActive(true);
            if (e.stoveState == StoveCounter.State.Resting)
            {

                animator.SetBool(IS_BURNING, false);
            }
            if (e.stoveState == StoveCounter.State.Burning)
            {

                animator.SetBool(IS_BURNING, true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
