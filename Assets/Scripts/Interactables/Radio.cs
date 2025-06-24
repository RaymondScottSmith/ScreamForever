using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interact
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
}
