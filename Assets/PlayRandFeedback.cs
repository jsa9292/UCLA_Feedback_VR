using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using UnityEngine;

public class PlayRandFeedback : MonoBehaviour
{
    public bool positive;
    public bool negative;
    private GameObject[] pool;
    public GameObject[] positives;
    public GameObject[] negatives;
    // Start is called before the first frame update
    void Start()
    {
        pool = new GameObject[0];
        if (positive) pool = pool.Concat(positives).ToArray();
        if (negative) pool = pool.Concat(negatives).ToArray();
    }
    public void GetRandomFeedback() { 
        int i = Random.Range(0, pool.Length);
        pool[i].SetActive(true);
    
    }
}
