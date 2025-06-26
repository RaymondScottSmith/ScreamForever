using UnityEngine;

public class TVKnobController : MonoBehaviour
{
    public TVController TV;
    public TVController.axis axis;
    public AudioClip click;
    public AudioSource audioSource;
    public bool locked;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.clip = click;
        audioSource.volume = 0.01f;
        audioSource.spatialBlend = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        TV = GameObject.FindFirstObjectByType<TVController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (TV == null) throw new System.Exception("TV Knob is not attached to a tv!");

        if (locked)
            return;
        if (Input.GetMouseButton(0))
        {
            if (TV.Tune(axis, TVController.dir.left))
            {
                transform.Rotate(0, 0, 36 * Time.deltaTime);
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                locked = true;
            }

            
            return;
        }

        /*

        if (Input.GetMouseButton(1))
        {
            if (TV.Tune(axis, TVController.dir.right))
            {
                transform.Rotate(0, 0, -36 * Time.deltaTime);
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            return;
        }
        */
    }

}
