using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperClip : Interact
{
    public MeshRenderer renderer;
    public Collider collider;
    

    private void Awake()
    {
        //renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }
    public override void Interaction()
    {
        readyToAdvance = true;
        //CanvasManager.Instance.InterruptDisplay(baseInteraction);
        IterationManager.Instance.ReadyToAdvance();
        renderer.enabled = false;
        collider.enabled = false;
        FindObjectOfType<TableNewspaper>().collider.enabled = true;

    }

    public void OfferNewPaper()
    {
        renderer.enabled = true;
        collider.enabled = true;
    }
    protected override void NextIteration(int newIter)
    {
        
        base.NextIteration(newIter);
        if (newIter == 2)
        {
            renderer.enabled = true;
            collider.enabled = true;
        }
        else if (newIter == 4)
        {
            readyToAdvance = false;
        }
        else
        {
            readyToAdvance = true;
        }
    }
}
