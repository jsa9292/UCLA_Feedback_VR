using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AudioAutoDisable : MonoBehaviour
{
    public Transform cam;
    public VisualEffect vfx;
    public AudioSource aud;
    public float waitTime;
    public float playTime;
    // Update is called once per frame
    void Update()
    {
        vfx.SetVector3("pos", cam.position);
        vfx.SetVector3("rot", cam.localEulerAngles);
        vfx.SetFloat("on", 1f);
        if (!aud.isPlaying)
        {
            playTime += Time.deltaTime;
            vfx.SetFloat("on", 0f);
        }
        if (playTime > waitTime)
        {
            playTime = 0;
            gameObject.SetActive(false);
        }
    }
}
