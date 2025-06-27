using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : Interact
{
    private Collider collider;
    public GameObject innerLighter;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
        innerLighter.SetActive(false);
    }
    public override void Interaction()
    {
        readyToAdvance = true;
        //CanvasManager.Instance.InterruptDisplay(baseInteraction);
        IterationManager.Instance.ReadyToAdvance();
        //renderer.enabled = false;
        collider.enabled = false;
        FindObjectOfType<FirstPersonController>().heldLighter.SetActive(true);
        gameObject.SetActive(false);
        //FindObjectOfType<TableNewspaper>().collider.enabled = true;

    }
    
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        switch (newIter)
        {
            case 12:
                collider.enabled = true;
                innerLighter.SetActive(true);
                break;
            default:
                readyToAdvance = true;
                break;
        }
    }
}
