using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedCounter;
    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;
    private const string OPEN_CLOSE = "OpenClose";
    private void Awake()
    {
        animator=GetComponent<Animator>();
    }
    void Start()
    {
        Player.playerInstance.OnSelectedCounterChanged += PlayerInstance_OnSelectedCounterChanged;
        containerCounter.OnContainerCounterInteract += ContainerCounter_OnContainerCounterInteract;
    }

    private void PlayerInstance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (baseCounter==e.selectedCounter)
        {
            ShowClearCounterSelected();
        }
        else
        {
            HideClearCounterSelected();
        }
    }
    private void ShowClearCounterSelected()
    {
        foreach(GameObject selectedCounterObject in selectedCounter){
            selectedCounterObject.SetActive(true);
        }
        
    }
    private void HideClearCounterSelected()
    {
        foreach (GameObject selectedCounterObject in selectedCounter)
        {
            selectedCounterObject.SetActive(false);
        }
    }
    private void ContainerCounter_OnContainerCounterInteract(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
