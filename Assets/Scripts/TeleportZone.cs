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
            var temp = other.GetComponent<FirstPersonController>();
            temp.characterController.enabled = false;
            Vector3 offset = other.transform.position - transform.position;
            Debug.Log(offset);
            Transform playerT = temp.transform;
            Vector3 playerEuler = playerT.rotation.eulerAngles;
            //temp.transform.rotation = Quaternion.Euler(temp.transform.rotation.eulerAngles + endLocation.rotation.eulerAngles);
            //temp.transform.position = endLocation.position + offset;
            /*
            temp.transform.rotation = endLocation.transform.rotation *
                                      Quaternion.Euler(
                                          temp.transform.rotation.eulerAngles - endLocation.rotation.eulerAngles  );
                                          */
            
            //Debug.Log(Quaternion.Euler(temp.transform.rotation.eulerAngles + (temp.transform.rotation.eulerAngles - endLocation.rotation.eulerAngles)));
            playerT.rotation = Quaternion.Euler(playerEuler + (endLocation.rotation.eulerAngles));
            temp.transform.position = endLocation.position + (endLocation.rotation * offset);
            
            temp.characterController.enabled = true;
            
        }
    }
}
