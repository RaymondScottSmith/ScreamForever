using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Iterable
{
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        if (newIter == 3)
        {
            StartFlickering();
        }
        else
        {
            readyToAdvance = true;
        }
    }

    private float timer;
    private float interval;
    public float flickerMax;
    public Light myLight;

    public bool flicker;
    private void Update()
    {
        if (flicker)
        {
            timer += Time.deltaTime;
            if (timer > interval)
            {
                myLight.enabled = !myLight.enabled;
                interval = Random.Range(0f, flickerMax);
                timer = 0;
            }
        }
        
    }

    public void StartFlickering()
    {
        flicker = true;
    }

    public void StopFlickering()
    {
        flicker = false;
        myLight.enabled = true;
    }
    
    
}
