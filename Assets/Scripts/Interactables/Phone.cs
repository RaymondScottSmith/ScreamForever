using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : Interact
{
    
    public List<string> iter1Messages;
    public List<string> iter4Messages;

    private LookAt lookAt;
    
    public bool needsReset;

    private void Awake()
    {
        lookAt = GetComponent<LookAt>();
    }

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
            
            case 4:
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

    public void ForceLook()
    {
        lookAt.ForceLook();
    }
    
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        switch (newIter)
        {
            case 0:
                break;
            case 1:
                break;
            case 4:
                needsReset = true;
                break;
            default:
                readyToAdvance = true;
                break;
        }
    }
    
}
