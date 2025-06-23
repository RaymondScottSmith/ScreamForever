using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject textPane;
    public TMP_Text infoText;
    private bool mousedOver;

    private void Update()
    {
        if (mousedOver && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,1.5f))
            {
                
                Grabable grabbed = hit.collider.GetComponent<Grabable>();
                grabbed.gameObject.SetActive(false);
                
                OnTriggerExit(hit.collider);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Grabber"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5f))
            {
                if (hit.collider.CompareTag("Grabber"))
                {
                    mousedOver = true;
                    Grabable grabbed = hit.collider.GetComponent<Grabable>();
                    if (grabbed.hasInfo)
                    {
                        textPane.SetActive(true);
                        infoText.text = grabbed.hoverInfo;
                    }
                    
                }
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabber"))
        {
            mousedOver = false;
            textPane.SetActive(false);
            infoText.text = "";
        }
    }
    
}
