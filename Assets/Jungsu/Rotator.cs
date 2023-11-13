using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rate;
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        if (lifeTime > 0) Invoke("Death", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rate * Time.deltaTime);
    }
    void Death() {
        GameObject.Destroy(gameObject);
    }
}
