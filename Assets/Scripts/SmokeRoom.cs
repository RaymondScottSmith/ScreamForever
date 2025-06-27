using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeRoom : Iterable
{
    public GameObject fireSource;

    private void Awake()
    {
        fireSource.SetActive(false);
    }
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        switch (newIter)
        {
            case 9:
                fireSource.SetActive(true);
                break;
            default:
                fireSource.SetActive(false);
                readyToAdvance = true;
                break;
        }
    }
}
