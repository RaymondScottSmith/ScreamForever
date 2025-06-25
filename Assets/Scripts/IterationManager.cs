using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterationManager : MonoBehaviour
{
    public static IterationManager Instance;
    public delegate void AdvanceIteration(int newIteration);
    public static event AdvanceIteration OnAdvance;
    public int currentIteration = 0;

    public List<Interact> interactables;

    public Door exitDoor;

    private void Awake()
    {
        if (IterationManager.Instance == null)
        {
            IterationManager.Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        if (currentIteration > 0)
        {
            currentIteration--;
            StartNextIteration();
        }
    }

    public void ReadyToAdvance()
    {
        exitDoor.locked = true;
        foreach (Interact inter in interactables)
        {
            if (!inter.readyToAdvance)
            {
                return;
            }
                
        }

        exitDoor.locked = false;
    }

    public void StartNextIteration()
    {
        currentIteration++;
        
        NextIteration();
        exitDoor.locked = true;
        ReadyToAdvance();
    }

    [ContextMenu("Advance")]
    public void NextIteration()
    {
        OnAdvance(currentIteration);
    }
    
}
