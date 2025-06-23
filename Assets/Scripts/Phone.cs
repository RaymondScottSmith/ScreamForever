using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : Interact
{
    [TextArea]
    public string iter1Message;

    
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
                    CanvasManager.Instance.WriteText(iter1Message);
                    needsReset = true;
                }
                else
                {
                    CanvasManager.Instance.InterruptDisplay(baseInteraction);
                }
                break;
            default:
                CanvasManager.Instance.InterruptDisplay(baseInteraction);
                break;
        }
        
    }
    
}
