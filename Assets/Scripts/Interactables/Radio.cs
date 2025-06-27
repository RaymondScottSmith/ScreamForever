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

    public AudioClip wasItWorth;

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
            
            case 10:
                CanvasManager.Instance.InterruptDisplay("They weren't supposed to be here!");
                break;
            case 11:
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

    private IEnumerator WorthIt()
    {
        musicSource.volume = 0f;
        scaresSource.clip = wasItWorth;
        scaresSource.loop = true;
        scaresSource.Play();
        while (IterationManager.Instance.currentIteration == 10)
        {
            yield return new WaitForSeconds(2f);
            scaresSource.pitch = 0.7f + Random.Range(0, 0.3f);
        }

        scaresSource.loop = false;
        scaresSource.Stop();
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
            case 10:
                readyToAdvance = true;
                StartCoroutine(WorthIt());
                break;
            case 11:
                readyToAdvance = true;
                musicSource.volume = 0f;
                break;
            case 12:
                readyToAdvance = true;
                musicSource.volume = 0f;
                break;
            default:
                playing = true;
                musicSource.volume = musicVolume;
                readyToAdvance = true;
                break;
        }
    }
}
