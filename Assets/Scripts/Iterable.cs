using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iterable : MonoBehaviour
{
    public int currentIter;
    public bool readyToAdvance;
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
        readyToAdvance = false;
        Debug.Log(name + " confirms next iteration.");
    }

}
