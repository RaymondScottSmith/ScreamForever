using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    private float timer = 0;
    public float interval = 0;
    public float flickerMax = 0.5f;
    private Light myLight;

    private void Awake()
    {
        myLight = GetComponent<Light>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            myLight.enabled = !myLight.enabled;
            interval = Random.Range(0f, flickerMax);
            timer = 0;
        }
    }
}
