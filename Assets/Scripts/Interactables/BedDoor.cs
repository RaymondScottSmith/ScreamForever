using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedDoor : Interact
{
    public Animator animator;
    public bool locked;
    public bool open;

    public LookAt bodyLookAt;

    public Collider triggerCollider;
    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }
    
    public override void Interaction()
    {
        switch (currentIter)
        {
            
            default:
                if (!locked)
                {
                    if (!open)
                    {
                        animator.SetTrigger("Open");
                        if (currentIter == 6)
                            bodyLookAt.ForceLook();
                    }
                        
                }
                readyToAdvance = true;
                //CanvasManager.Instance.InterruptDisplay(baseInteraction);
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
                locked = false;
                break;
            case 6:
                animator.SetTrigger("Prop");
                locked = false;
                break;
            default:
                animator.SetTrigger("Close");
                locked = true;
                break;
        }
    }
}
