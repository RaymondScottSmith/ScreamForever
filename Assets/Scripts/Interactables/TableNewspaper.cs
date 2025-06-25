using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableNewspaper : Interact
{
    public MeshRenderer renderer;
    public Collider collider;

    public Lamp attachedLamp;

    public List<Material> newspaperMaterials;

    private int paperNumber = 0;

    public Renderer pageRenderer;

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
        collider.enabled = false;
        attachedLamp.StopFlickering();
        AddNewPiece();

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

    [ContextMenu("Add Piece")]
    public void AddNewPiece()
    {
        paperNumber++;
        if (paperNumber < newspaperMaterials.Count)
            pageRenderer.material = newspaperMaterials[paperNumber];
    }
}
