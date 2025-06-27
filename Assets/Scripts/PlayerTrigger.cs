using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : Iterable
{

    public bool triggered;
    public int iterTrigger;

    public GameObject item;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player") && currentIter == iterTrigger)
        {
            triggered = true;
            item.SetActive(true);
        }
    }
    
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        switch (newIter)
        {
            default:
                item.SetActive(false);
                break;
        }
    }
}
