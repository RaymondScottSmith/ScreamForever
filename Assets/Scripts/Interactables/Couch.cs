using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couch : Interact
{
    public Animator gasCanAnimator;
    private bool gasSoaked;

    private void Awake()
    {
        gasSoaked = false;
    }
    public override void Interaction()
    {
        switch (currentIter)
        {
            case 5:
                FirstPersonController fpc = FindObjectOfType<FirstPersonController>();
                
                if (fpc.heldGascan.activeSelf && !gasSoaked)
                {
                    gasCanAnimator.GetComponent<LookAt>().ForceLook();
                    fpc.canMove = false;
                    fpc.heldGascan.SetActive(false);
                    gasCanAnimator.SetTrigger("Pour");
                    gasSoaked = true;
                    StartCoroutine(fpc.ReturnGasCan());
                    readyToAdvance = true;
                    IterationManager.Instance.ReadyToAdvance();
                    baseInteraction = "Now it smells right...";
                }
                else
                {
                    CanvasManager.Instance.InterruptDisplay(baseInteraction);
                }
                break;
            default:
                readyToAdvance = true;
                CanvasManager.Instance.InterruptDisplay(baseInteraction);
                break;
        }
        IterationManager.Instance.ReadyToAdvance();
        
    }

    
    
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        switch (newIter)
        {
            case 0:
                break;
            case 5:
                break;
            default:
                readyToAdvance = true;
                break;
        }
    }
}
