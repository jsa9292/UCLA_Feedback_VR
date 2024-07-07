using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvnAlign : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = new Vector3(transform.GetChild(i).localPosition.x, 0f, transform.GetChild(i).localPosition.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
