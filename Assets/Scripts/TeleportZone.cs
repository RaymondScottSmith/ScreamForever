using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    public Transform endLocation;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IterationManager.Instance.StartNextIteration();
            var temp = other.GetComponent<FirstPersonController>();
            temp.characterController.enabled = false;
            Vector3 offset = other.transform.position - transform.position;
            Transform playerT = temp.transform;
            Vector3 playerEuler = playerT.rotation.eulerAngles;
            
            playerT.rotation = Quaternion.Euler(playerEuler + (endLocation.rotation.eulerAngles));
            temp.transform.position = endLocation.position + (endLocation.rotation * offset);
            
            temp.characterController.enabled = true;
        }
    }
}
