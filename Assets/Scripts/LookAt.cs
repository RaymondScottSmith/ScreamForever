using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public float lookTime = 1f;

    public bool noMorForce;

    public void ForceLook(float timer = -1)
    {
        if (timer > 0)
            lookTime = timer;

        StartCoroutine(LookNow());
    }
    [ContextMenu("Force Look")]
    public void ForcedLook()
    {
        if (!noMorForce)
        {
            noMorForce = true;
            StartCoroutine(LookNow());
        }
        
    }

    private IEnumerator LookNow()
    {
        Debug.Log("starting look lock");
        FirstPersonController fpc = FindObjectOfType<FirstPersonController>();
        fpc.characterController.enabled = false;
        fpc.canMove = false;
        Vector3 direction = (transform.position - fpc.playerCamera.transform.position).normalized;
        fpc.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        fpc.playerCamera.transform.rotation = Quaternion.LookRotation(direction);
        
        yield return new WaitForSeconds(lookTime);
        fpc.characterController.enabled = true;
        fpc.canMove = true;
        Debug.Log("ending look lock");
    }

    
}
