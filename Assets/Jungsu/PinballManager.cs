using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballManager : MonoBehaviour
{
    public GameObject[] blocks;
    public Rigidbody ball;
    public Transform ballResetPos;
    public Transform basket;
    public float speed;
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
        if (TrialManager.instance.controllerTrigger > 0.5f) Reset();
        basket.localPosition = new Vector3(TrialManager.instance.controllerPad.x * basketMax, initialPos.y,initialPos.z);
    }
    public bool reset;
    private void Reset()
    {
        ball.position = ballResetPos.position;
        ball.velocity = Vector3.up * speed;
        foreach (GameObject g in blocks) {
            g.SetActive(true);
        }
        reset = false;
    }
}
