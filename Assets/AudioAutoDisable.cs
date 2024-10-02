using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAutoDisable : MonoBehaviour
{
    public AudioSource aud;
    public float waitTime;
    public float playTime;
    // Update is called once per frame
    void Update()
    {
        if (!aud.isPlaying)
        {
            playTime += Time.deltaTime;
        }
        if (playTime > waitTime)
        {
            playTime = 0;
            gameObject.SetActive(false);
        }
    }
}
