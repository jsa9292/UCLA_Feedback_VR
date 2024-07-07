using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballManager : MonoBehaviour
{    
    public GameObject[] blocks;
    public Rigidbody ball;
    public Transform ballResetPos;
    public Transform basket;
    public float basketspeed;
    public float ballSpeed;
    private Vector3 initialPos;
    public float basketMax;
     
    // Start is called before the first frame update
    void OnEnable()
    {
        Reset();
        initialPos = basket.localPosition;
    }
     
    // Update is called once per frame
    void Update()
    {
        float xMove = 0;
        xMove += TrialManager.instance.controllerPad.x > 0.2f ? 1:0;
        xMove += TrialManager.instance.controllerPad.x < -0.2f ? -1:0;
        xMove *= basketspeed * Time.deltaTime;
        if (TrialManager.instance.controllerTrigger > 0.5f) Reset();
        basket.localPosition = new Vector3(Mathf.Clamp(basket.localPosition.x+xMove * Time.deltaTime, -basketMax,basketMax), initialPos.y,initialPos.z);
    }
     
    public bool reset;
    private void Reset()
    {
        ball.position = ballResetPos.position;
        ball.velocity = Vector3.up * ballSpeed;
        foreach (GameObject g in blocks) {
            g.SetActive(true);
        }
        reset = false;
    }
}    
