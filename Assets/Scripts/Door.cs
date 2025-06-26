using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Iterable
{
    public bool locked;
    public bool lockAfterPlayer;
    public Transform afterLocation;
    public bool open;
    public Animator animator;
    public bool permaLocked;

    private float playerEnterDist;

    public bool lRoomEntrance;
    public Phone phone;
    public AudioClip lockedEffect;
    public AudioClip openingEffect;
    public AudioClip closingEffect;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        if (locked)
        {
            Debug.Log("Add door is locked message");
            AudioSource.PlayClipAtPoint(lockedEffect, transform.position);
            return;
        }
        else
        {
            animator.ResetTrigger("Close");
            animator.SetTrigger("Open");
            AudioSource.PlayClipAtPoint(openingEffect, transform.position);
            open = true;
        }
    }

    public void CloseDoor(bool playAudio = true)
    {
        animator.ResetTrigger("Open");
        animator.SetTrigger("Close");
        open = false;

        if (playAudio)
        {
            AudioSource.PlayClipAtPoint(closingEffect, transform.position);
        }

    }

    public void ChangeLockedState(bool isLocked)
    {
        locked = isLocked;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (lockAfterPlayer && other.CompareTag("Player"))
        {
            playerEnterDist = Vector3.Distance(other.transform.position, afterLocation.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.ResetTrigger("Open");
            open = false;
            animator.SetTrigger("Close");
            AudioSource.PlayClipAtPoint(closingEffect, transform.position);
            if (lockAfterPlayer)
            {
                if (playerEnterDist > Vector3.Distance(other.transform.position, afterLocation.position))
                {
                    locked = true;
                }
            }

            if (currentIter == 1 && lRoomEntrance)
            {
                phone.ForceLook();
            }
        }
    }

    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        CloseDoor(false);
        Debug.Log("Do extra door stuff here");
        switch (newIter)
        {
            default:
                if (!permaLocked)
                    locked = false;
                break;
        }
        
    }
}
