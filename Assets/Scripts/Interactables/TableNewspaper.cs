using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableNewspaper : Interact
{
    public MeshRenderer renderer;
    public Collider collider;

    public Lamp attachedLamp;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }
    public override void Interaction()
    {
        readyToAdvance = true;
        //CanvasManager.Instance.InterruptDisplay(baseInteraction);
        IterationManager.Instance.ReadyToAdvance();
        renderer.enabled = false;
        collider.enabled = false;
        attachedLamp.StopFlickering();

    }
    protected override void NextIteration(int newIter)
    {
        
        base.NextIteration(newIter);
        if (newIter == 3)
        {
            renderer.enabled = true;
            collider.enabled = true;
        }
        else
        {
            readyToAdvance = true;
        }
    }
}
