using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class GasCan : Interact
{
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }
    public override void Interaction()
    {
        readyToAdvance = true;
        //CanvasManager.Instance.InterruptDisplay(baseInteraction);
        IterationManager.Instance.ReadyToAdvance();
        //renderer.enabled = false;
        collider.enabled = false;
        FindObjectOfType<FirstPersonController>().heldGascan.SetActive(true);
        gameObject.SetActive(false);
        //FindObjectOfType<TableNewspaper>().collider.enabled = true;

    }
    
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        switch (newIter)
        {
            case 5:
                collider.enabled = true;
                break;
            default:
                readyToAdvance = true;
                break;
        }
    }
}
