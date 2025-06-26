using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Iterable
{
    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
    public void Hide()
    {
        renderer.enabled = false;
    }

    public void Reveal()
    {
        renderer.enabled = true;
    }
    
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        switch (newIter)
        {

            case 6:
                Reveal();
                break;
            default:
                Hide();
                break;
        }
    }
}
