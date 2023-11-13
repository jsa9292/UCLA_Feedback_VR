using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowFieldParticle : MonoBehaviour
{
    public float _movespeed;
    public int _audioBand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
        if (float.IsNaN(_movespeed))
        {
            Debug.Log("particle flowfield _movespeed NaN");
            return;
        }
        else this.transform.position += transform.forward* _movespeed * Time.deltaTime;
    }

    public void ApplyRotation(Vector3 rotation, float rotateSpeed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(rotation.normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
