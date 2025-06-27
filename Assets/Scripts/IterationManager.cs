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

    private bool playedSound;
    private AudioSource unlockSound;

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
        unlockSound = GetComponent<AudioSource>();
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
                Debug.Log(inter.name + "Not ready yet");
                return;
            }
                
        }

        if (!playedSound)
        {
            unlockSound.Play();
            playedSound = true;
        }
        if (currentIteration != 12)
            exitDoor.locked = false;
        else
        {
            exitDoor.locked = true;
        }
        
    }

    public void StartNextIteration()
    {
        currentIteration++;
        
        NextIteration();
        exitDoor.locked = true;
        playedSound = false;
        ReadyToAdvance();
    }

    [ContextMenu("Advance")]
    public void NextIteration()
    {
        OnAdvance(currentIteration);
    }
    
}
