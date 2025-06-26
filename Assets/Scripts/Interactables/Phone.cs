using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : Interact
{
    
    public List<string> iter1Messages;
    public List<string> iter4Messages;

    private LookAt lookAt;
    
    public bool needsReset;

    public GameObject phoneUI;
    public AudioSource phoneAudio;

    public AudioClip ringSound;
    private bool gasSoaked;
    public Animator gasAnimator;

    private void Awake()
    {
        lookAt = GetComponent<LookAt>();
        phoneUI.GetComponent<PhoneUIController>().phone = this;
        phoneAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
    }
    public override void Interaction()
    {
        switch (currentIter)
        {
            case 1:
                if (!needsReset)
                {
                    StopLoop();
                    CanvasManager.Instance.WriteMultipleTexts(iter1Messages, true);
                    needsReset = true;
                    readyToAdvance = true;
                }
                else
                {
                    CanvasManager.Instance.InterruptDisplay(baseInteraction);
                }
                break;
            
            case 4:
                //readyToAdvance = true;
                //CanvasManager.Instance.InterruptDisplay(baseInteraction);
                phoneUI.SetActive(true);
                break;
            
            case 5:
                FirstPersonController fpc = FindObjectOfType<FirstPersonController>();
                
                if (gasSoaked)
                {
                    
                }
                else if (fpc.heldGascan.activeSelf)
                {
                    gasAnimator.GetComponent<LookAt>().ForceLook();
                    fpc.canMove = false;
                    fpc.heldGascan.SetActive(false);
                    gasAnimator.SetTrigger("Pour");
                    gasSoaked = true;
                    StartCoroutine(fpc.ReturnGasCan());
                }
                readyToAdvance = true;
                break;
            default:
                readyToAdvance = true;
                //CanvasManager.Instance.InterruptDisplay(baseInteraction);
                phoneUI.SetActive(true);
                break;
        }
        IterationManager.Instance.ReadyToAdvance();
        
    }

    private IEnumerator DelaySound()
    {
        PlayLoop(ringSound);
        yield return new WaitForSeconds(0.25f);
        lookAt.ForceLook();
    }

    public void ForceLook()
    {
        StartCoroutine(DelaySound());
    }

    public void PlayOneShot(AudioClip clip)
    {
        phoneAudio.PlayOneShot(clip);
    }

    public void PlayLoop(AudioClip clip)
    {
        phoneAudio.loop = true;
        phoneAudio.clip = clip;
        phoneAudio.Play();
    }

    public void StopLoop()
    {
        phoneAudio.Stop();
        phoneAudio.loop = false;
    }
    
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        switch (newIter)
        {
            case 0:
                break;
            case 1:
                break;
            case 4:
                needsReset = true;
                break;
            case 5:
                readyToAdvance = false;
                break;
            default:
                readyToAdvance = true;
                break;
        }
    }

    public void OutgoingCall()
    {
        StartCoroutine(OutgoingDelay());
    }

    private IEnumerator OutgoingDelay()
    {
        yield return new WaitForSeconds(1f);
        StopLoop();
        yield return new WaitForSeconds(0.5f);
        if (currentIter == 4)
        {
            CanvasManager.Instance.WriteMultipleTexts(iter4Messages, true);
            FindObjectOfType<NewspaperClip>().OfferNewPaper();
        }
        readyToAdvance = true;
        IterationManager.Instance.ReadyToAdvance();
    }

}
