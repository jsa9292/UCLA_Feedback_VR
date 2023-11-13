using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerProximity : MonoBehaviour
{
    public Transform controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float dist;
    public float threshold;
    public Transform wrist;
    public SkinnedMeshRenderer smr;
    // Update is called once per frame
    void Update()
    {
        dist = (wrist.position - controller.position).magnitude;
        if(dist>threshold) smr.enabled = true;
        else smr.enabled = false;



    }
}
