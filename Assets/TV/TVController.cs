using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering;

public class TVController : Iterable
{
    public MeshRenderer renderer;
    public GameObject tuneHKnob; //horizantal image tuning
    public GameObject tuneVKnob; //vertical image tuning

    public Vector2 axisRange = new Vector2(-10, 10);
    public float vValue = 10;
    public float hValue = -10;
    public float tuningSpeed = 100f;

    public Material screenMaterial;
    public Material staticMaterial; //used when the TV isn't showing anything
    public Material offMaterial;
    public Texture2D screenTexture;

    private float graceRange = 0.25f;

    private bool lockH = false;
    private bool lockV = false;

    public AudioClip hum;

    private GameObject soundEffect;
    public bool isOn = true;

    public enum axis { H, V }
    public enum dir { left, right };

    public AudioClip staticNoise;
    public AudioClip staticScream;

    // Start is called before the first frame update
    void Start()
    {

        screenMaterial.SetFloat("_ScrollSpeed", vValue);
        screenMaterial.SetFloat("_Flicker", hValue);

        soundEffect = new GameObject("TVSound");
        soundEffect.transform.position = transform.position;
        AudioSource humSource = soundEffect.AddComponent<AudioSource>();
        humSource.loop = true;
        humSource.volume = 0.1f;
        humSource.spatialBlend = 1;
        humSource.clip = hum;
        //humSource.Play();


        SetToPuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Tune(axis ax, dir direction)
    {
        float tuningAmount = tuningSpeed * Time.deltaTime;

        if (direction == dir.left)
        {
            tuningAmount *= -1;
        }


        if (ax == axis.H)
        {
            hValue += tuningAmount;
            hValue = Mathf.Clamp(hValue, axisRange.x, axisRange.y);
            lockH = Mathf.Abs(hValue) <= graceRange;
        }
        else
        {
            vValue += tuningAmount;
            vValue = Mathf.Clamp(vValue, axisRange.x, axisRange.y);
            lockV = Mathf.Abs(vValue) <= graceRange;
        }

        if (lockH)
        {
            screenMaterial.SetFloat("_Flicker", 0.01f);
        } else
        {
            screenMaterial.SetFloat("_Flicker", hValue);
        }

        if (lockV)
        {
            screenMaterial.SetFloat("_ScrollSpeed", 0);
        } else
        {
            screenMaterial.SetFloat("_ScrollSpeed", vValue);
        }


        switch (ax)
        {
            case axis.H:
                return !(Mathf.Approximately(hValue, axisRange.x));
                
            case axis.V:
                return !(Mathf.Approximately(vValue, axisRange.y));
        }


        return false;
        
    }

    public void TVState(bool state)
    {
        //@todo on/off mechanics for TV

        if (soundEffect == null) return;

        if (state)
        {
            soundEffect.GetComponent<AudioSource>().Play();
        } else
        {
            soundEffect.GetComponent<AudioSource>().Stop();
        }

        isOn = state;
    }

    public void SetToStatic()
    {
        Material[] materials = renderer.materials;
        materials[1] = new Material(staticMaterial);
        renderer.materials = materials;

        if (soundEffect != null)
        {
            soundEffect.GetComponent<AudioSource>().clip = staticNoise;
            soundEffect.GetComponent<AudioSource>().Play();
        }
    }

    public void SetToPuzzle()
    {
        Material[] materials = renderer.materials;
        materials[1] = new Material(screenMaterial);
        renderer.materials = materials;

        if (soundEffect != null)
        {
            soundEffect.GetComponent<AudioSource>().clip = hum;
            soundEffect.GetComponent<AudioSource>().Play();
        }
    }
}
