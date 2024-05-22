using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;
    private const string CUT = "Cut";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        cuttingCounter.OnCuttingRecipeInteract += CuttingCounter_OnCuttingRecipeInteract;
    }

    private void CuttingCounter_OnCuttingRecipeInteract(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }

     
}
