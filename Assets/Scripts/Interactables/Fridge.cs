using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interact
{
    private Animator fridgeAnimator;

    private void Awake()
    {
        fridgeAnimator = GetComponent<Animator>();
    }
    public override void Interaction()
    {
        switch (currentIter)
        {
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
            default:
                readyToAdvance = true;
                break;
        }
    }

    public void OpenFridge()
    {
        fridgeAnimator.SetTrigger("Open");
    }

    public void CloseFridge()
    {
        fridgeAnimator.SetTrigger("Close");
    }
}
