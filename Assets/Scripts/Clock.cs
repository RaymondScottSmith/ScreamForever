using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Clock : Iterable
{
    public List<string> times;
    private string currentTime;

    private void Start()
    {
        currentTime = times[0];
    }
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        if (times.Count >= newIter)
        {
            currentTime = times[newIter];
        }
        else
        {
            currentTime = times[0];
        }
    }
}
