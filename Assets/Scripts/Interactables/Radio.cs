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

    private bool needsReset = true;

    public List<string> messages;
    private void Awake()
    {
        musicVolume = musicSource.volume;
    }
    public override void Interaction()
    {
        switch (currentIter)
        {
            case 7:
                if (needsReset)
                {
                    readyToAdvance = true;
                    needsReset = false;
                    //scaresSource.clip = screams;
                    scaresSource.PlayOneShot(screams);
                    CanvasManager.Instance.WriteMultipleTexts(messages,true);
                    baseInteraction = "I didn't know...";
                }
                else
                {
                    CanvasManager.Instance.InterruptDisplay(baseInteraction);
                }
                break;
            
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
                musicSource.clip = policeChatter;
                musicSource.volume = musicVolume;
                musicSource.Play();
                break;
            case 8:
                musicSource.clip = baseSong;
                musicSource.volume = musicVolume;
                musicSource.pitch = 0.6f;
                musicSource.Play();
                readyToAdvance = true;
                break;
            
            case 9:
                playing = false;
                musicSource.volume = 0f;
                readyToAdvance = true;
                break;
            default:
                playing = true;
                musicSource.volume = musicVolume;
                readyToAdvance = true;
                break;
        }
    }
}
