using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using UnityEngine;

public class PlayRandFeedback : MonoBehaviour
{
    public bool initialized;
    public bool positive;
    public bool negative;
    public List<GameObject> pool;
    public GameObject[] positives;
    public GameObject[] negatives;
    // Start is called before the first frame update
    public void Init()
    {
        if (positive) {
            foreach (GameObject f in positives)
            {
                pool.Add(f);
            }
        }
        if (negative) {
            foreach (GameObject f in negatives)
            {
                pool.Add(f);
            }
        }
        initialized = true;
    }
    public void GetRandomFeedback() { 
        int i = Random.Range(0, pool.Count);
        pool[i].SetActive(true);
    
    }
}
