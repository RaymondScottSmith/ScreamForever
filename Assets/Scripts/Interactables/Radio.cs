using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interact
{
    public AudioSource musicSource;
    public AudioSource scaresSource;

    public AudioClip baseSong;
    public AudioClip policeChatter;
    public AudioClip speech;
    public AudioClip screams;

    private bool playing = true;
    private float musicVolume;
    private void Awake()
    {
        musicVolume = musicSource.volume;
    }
    public override void Interaction()
    {
        switch (currentIter)
        {
            default:
                if (playing)
                {
                    musicSource.volume = 0f;
                    playing = false;
                }
                else
                {
                    playing = true;
                    musicSource.volume = musicVolume;
                }
                readyToAdvance = true;
                CanvasManager.Instance.InterruptDisplay(baseInteraction);
                break;
        }
        IterationManager.Instance.ReadyToAdvance();
        
    }
    
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        switch (newIter)
        {
            case 0:
                break;
            case 7:
                break;
            default:
                playing = true;
                musicSource.volume = musicVolume;
                readyToAdvance = true;
                break;
        }
    }
}
