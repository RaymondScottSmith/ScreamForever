using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool locked;
    public bool open;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        if (locked)
        {
            Debug.Log("Add door is locked message");
            return;
        }
        else
        {
            animator.ResetTrigger("Close");
            animator.SetTrigger("Open");
            open = true;
        }
    }

    public void ChangeLockedState(bool isLocked)
    {
        locked = isLocked;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.ResetTrigger("Open");
            open = false;
            animator.SetTrigger("Close");
        }
    }
}
