using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering;

public class TVController : Interact
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
    public Material fullAdd;
    public Texture2D screenTexture;

    private float graceRange = 0.1f;

    private bool lockH = false;
    private bool lockV = false;

    public AudioClip hum;
    private AudioSource audioSource;

    private GameObject soundEffect;
    public bool isOn = true;

    public enum axis { H, V }
    public enum dir { left, right };

    public AudioClip staticNoise;
    public AudioClip staticScream;

    private Collider tvCollider;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        tvCollider = GetComponent<Collider>();
    }


    // Start is called before the first frame update
    void Start()
    {

        screenMaterial.SetFloat("_ScrollSpeed", vValue);
        screenMaterial.SetFloat("_Flicker", hValue);
        soundEffect = new GameObject("TVSound");

        Material[] materials = renderer.materials;
        materials[1] = offMaterial;
        renderer.materials = materials;

        soundEffect = new GameObject("TVHum");
        soundEffect.transform.position = transform.position;
        AudioSource humSource = soundEffect.AddComponent<AudioSource>();
        humSource.loop = true;
        humSource.volume = 1f;
        humSource.spatialBlend = 1;
        humSource.clip = hum;
        humSource.Play();
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
            if (hValue < axisRange.x)
            {
                hValue = axisRange.y;
            }
            Debug.Log(hValue);
            //hValue = Mathf.Clamp(hValue, axisRange.x, axisRange.y);
            lockH = Mathf.Abs(hValue) <= graceRange;
            if (lockH)
            {
                screenMaterial.SetFloat("_Flicker", 0f);
            } else
            {
                screenMaterial.SetFloat("_Flicker", hValue);
            }
            if (lockH && lockV)
            {
                ChangeMaterial(fullAdd);
            }
            if (lockH)
                return false;
        }
        else
        {
            vValue += tuningAmount;
            if (vValue < axisRange.x)
            {
                vValue = axisRange.y;
            }
            Debug.Log(vValue);
            //vValue = Mathf.Clamp(vValue, axisRange.x, axisRange.y);
            lockV = Mathf.Abs(vValue) <= graceRange;
            if (lockV)
            {
                screenMaterial.SetFloat("_ScrollSpeed", 0);
            } else
            {
                screenMaterial.SetFloat("_ScrollSpeed", vValue);
            }

            if (lockH && lockV)
            {
                ChangeMaterial(fullAdd);
            }
            if (lockV)
                return false;
        }

        if (lockH)
        {
            screenMaterial.SetFloat("_Flicker", 0.01f);
        } else
        {
            screenMaterial.SetFloat("_Flicker", hValue);
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
  
    protected override void NextIteration(int newIter)
    {
        base.NextIteration(newIter);
        switch (newIter)
        {
            case 4:
                ChangeMaterial(screenMaterial);
                tvCollider.enabled = false;
                readyToAdvance = true;
                break;
            
            case 5:
                ChangeMaterial(offMaterial);
                tvCollider.enabled = true;
                readyToAdvance = true;
                break;

            default:
                ChangeMaterial(offMaterial);
                readyToAdvance = true;
                break;
        }
    }

    public void StartLoopingAudio(AudioClip clip)
    {
        audioSource.loop = true;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }

    public override void Interaction()
    {
        base.Interaction();
        isOn = !isOn;
        if (isOn)
        {
            switch (currentIter)
            {
                default:
                    ChangeMaterial(staticMaterial);
                    StartLoopingAudio(hum);
                    readyToAdvance = true;
                    break;
            }
        }
        else
        {
            ChangeMaterial(offMaterial);
            StopAudio();
        }

    }

    public void ChangeMaterial(Material newMaterial)
    {
        Debug.Log("Should be changing material");
        Material[] materials = renderer.materials;
        materials[1] = newMaterial;
        renderer.materials = materials;
    }
}
