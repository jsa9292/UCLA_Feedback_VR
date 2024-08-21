using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    private Vector3 magSum = Vector3.one;
    public void Magnify(float dir) {
        magSum += Vector3.one * dir;
        if (magSum.magnitude > 1f) transform.localScale = magSum;
        else magSum = Vector3.one;
    }
}
