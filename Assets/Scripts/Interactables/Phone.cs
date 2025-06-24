using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : Interact
{
    
    public List<string> iter1Messages;

    
    public bool needsReset;
    

    private void Start()
    {
    }
    public override void Interaction()
    {
        switch (currentIter)
        {
            case 1:
                if (!needsReset)
                {
                    CanvasManager.Instance.WriteMultipleTexts(iter1Messages, true);
                    needsReset = true;
                    readyToAdvance = true;
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
    
}
