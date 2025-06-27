using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOut : Iterable
{
    public List<Light> orangeLights;

    public List<Light> outLights;

    public void TurnOffLights()
    {
        foreach (Light light in outLights)
        {
            light.gameObject.SetActive(false);
        }
        foreach (Light light in orangeLights)
        {
            light.gameObject.SetActive(true);
        }
    }

    public void TurnOnLights()
    {
        foreach (Light light in outLights)
        {
            light.gameObject.SetActive(true);
        }
        foreach (Light light in orangeLights)
        {
            light.gameObject.SetActive(false);
        }
    }
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        switch (newIter)
        {
            case 11:
                TurnOffLights();
                readyToAdvance = true;
                break;
            case 12:
                TurnOnLights();
                readyToAdvance = true;
                break;
            default:
                
                readyToAdvance = true;
                break;
        }
    }

}
