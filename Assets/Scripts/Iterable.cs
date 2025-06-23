using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iterable : MonoBehaviour
{
    protected int currentIter;
    private void OnEnable()
    {
        IterationManager.OnAdvance += NextIteration;
    }

    private void OnDisable()
    {
        IterationManager.OnAdvance -= NextIteration;
    }

    protected virtual void NextIteration(int newIter)
    {
        currentIter = newIter;
        
        Debug.Log(name + " confirms next iteration.");
    }

}
