using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : Interact
{
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
}
