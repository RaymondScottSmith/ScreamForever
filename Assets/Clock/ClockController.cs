using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    public TMP_Text face;
    // Start is called before the first frame update
    void Start()
    {
        face.text = "18:00";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTime(string time)
    {
        face.text = time;
    }
}
