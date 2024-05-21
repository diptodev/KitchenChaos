using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
private Animator animator;
    [SerializeField] private ContainerCounter containerCounter;
    private const string OPEN_CLOSE = "OpenClose";
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containerCounter.OnContainerCounterInteract += ContainerCounter_OnContainerCounterInteract;
    }

    private void ContainerCounter_OnContainerCounterInteract(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
