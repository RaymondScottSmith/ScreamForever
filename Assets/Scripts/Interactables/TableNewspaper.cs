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
    public bool waiting;

    public GameObject facePaper;

    private void Awake()
    {
        //renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    public override void Interaction()
    {
            
            if (paperNumber == 6)
            {
                ShowNewspaper();
                return;
            }
            readyToAdvance = true;
            //CanvasManager.Instance.InterruptDisplay(baseInteraction);
            IterationManager.Instance.ReadyToAdvance();
            collider.enabled = false;
            if (currentIter != 11)
                attachedLamp.StopFlickering();
            AddNewPiece();
        
            
    }

    public void ShowNewspaper()
    {
        FirstPersonController fpc = FindObjectOfType<FirstPersonController>();
        facePaper.SetActive(true);
        fpc.canMove = false;
    }
    protected override void NextIteration(int newIter)
    {
        
        base.NextIteration(newIter);
        if (newIter == 3)
        {
            renderer.enabled = true;
            collider.enabled = true;
        }
        else if (newIter == 4)
        {
            readyToAdvance = false;
        }
        else if (newIter == 6)
            readyToAdvance = false;
        else if (newIter == 8)
            readyToAdvance = false;
        else if (newIter == 11)
            readyToAdvance = false;
        else if (newIter == 12)
        {
            readyToAdvance = false;
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
        
        if (paperNumber == 6)
        {
            collider.enabled = true;
        }
    }

    public void WaitingForNewPiece()
    {
        waiting = true;
        readyToAdvance = false;
    }
}
