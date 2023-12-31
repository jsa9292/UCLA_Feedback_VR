using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isLocked = false;
    private bool hasInstantiated = false;
    private GameObject targetObj;
    private Game2048Manager GM;
    public float leftLimit;
    public float rightLimit;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GM = Game2048Manager.instance;
        try { Destroy(GetComponent<LineRenderer>()); } catch (System.Exception) { }

    }
    private void OnTriggerStay(Collider other)
    {
        if (isLocked == false)
        {
            if (other.name == gameObject.name)
            {
                //Debug.Log("Found Matching ball!");

                if (other.gameObject.GetComponent<Ball>().isLocked == true)
                {
                    return; //Abort, other ball already locked!
                }
                isLocked = true;
                other.gameObject.GetComponent<Ball>().isLocked = true;
                Debug.Log("Locked");
                targetObj = other.gameObject;
            }
            if (other.gameObject.name == "GameOverBarrier")
            {
                if (rb && rb.velocity.magnitude <= 0.1f)
                {
                    GM.GameOver();
                    print(other.gameObject.name);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hasInstantiated == false && other.tag == "Ball" || other.tag == "Border" && gameObject.transform.parent == null)
        {
            hasInstantiated = true;
        }
    }

    private void Update()
    {
        if (targetObj == null) return;
        if (isLocked == true)
        {
            float step = 5f * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, targetObj.transform.position, step);
        }
        if (isLocked == true && Vector3.Distance(transform.position, targetObj.transform.position) <= 0.1f)
        {
            instantiateNew();
        }
    }

    private void instantiateNew()
    {
        Destroy(targetObj);
        GameObject ballToInstantiate = null;
        for (int i = 0; i < GM.balls.Length; i++)
        {
            if (this.name == GM.balls[i].name)
            {
                if (i++ > GM.balls.Length)
                {
                    Debug.Log("This is the max value ball can have");
                    Destroy(this.gameObject);
                }
                else
                {
                    ballToInstantiate = GM.balls[i++];
                }
            }
        }
        if (ballToInstantiate != null)
        {
            GameObject ball = Instantiate(ballToInstantiate, this.transform.position, this.transform.rotation) as GameObject;
            /*if(GM.shrinkBallSizes > 1)
                ball.transform.localScale /= GM.shrinkBallSizes;
            else if (GM.shrinkBallSizes < 0)
                ball.transform.localScale *= -GM.shrinkBallSizes;*/
            ball.name = ballToInstantiate.name;
            //ball.GetComponent<TrailRenderer>().startWidth = ball.transform.localScale.x;
            //ball.GetComponent<TrailRenderer>().endWidth = (ball.transform.localScale.x / 2f);
            GM.Merging(transform.name);
            Ball ballComponent;
            ball.TryGetComponent(out ballComponent);
            ballComponent.enabled = true;
            //Debug.Log("Instantaiated");
        }
        //merge
        Destroy(this.gameObject);
    }

    
}
