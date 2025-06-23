using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVKnobController : MonoBehaviour
{
    public TVController TV;
    public TVController.axis axis;

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

        if (Input.GetMouseButton(0))
        {
            TV.Tune(axis, TVController.dir.left);
            return;
        }


        if (Input.GetMouseButton(1))
        {
            TV.Tune(axis, TVController.dir.right);
            return;
        }


    }

}
