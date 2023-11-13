using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public MeshRenderer[] targets;
    public Color color;
    public bool random;
    public bool each;
    // Start is called before the first frame update
    void OnEnable()
    {
        foreach (MeshRenderer mr in targets) {

            if (random)
            {
                float h = Random.Range(0f, 1f);
                float s = 0.7f;
                float v = 1f;


                mr.material.color = Color.HSVToRGB(h, s, v);
            }
            else {

                mr.material.color = color;
            }
        }
    }
}
