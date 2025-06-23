using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterationManager : MonoBehaviour
{
    public static IterationManager Instance;
    public delegate void AdvanceIteration(int newIteration);
    public static event AdvanceIteration OnAdvance;
    public int currentIteration = 0;

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

    public void StartNextIteration()
    {
        currentIteration++;
        NextIteration();
    }

    [ContextMenu("Advance")]
    public void NextIteration()
    {
        OnAdvance(currentIteration);
    }
    
}
