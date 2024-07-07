using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiaterChild : MonoBehaviour
{
    public Instantiater instantiater;
    public Rigidbody rb;

    private Transform leftLimit;
    private Transform rightLimit;
    private Touch touch;
    private LineRenderer lineRenderer;

    private void Start()
    {
        leftLimit = instantiater.leftLimit;
        rightLimit = instantiater.rightLimit;
        Ball ball = GetComponent<Ball>();
        ball.isLocked = true;
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }


    public Vector3 prevPos;
    public Vector3 deltaPos;
    public void Update()
    {
        //if (Input.GetMouseButtonDown(0)) {
        //    prevPos = Input.mousePosition;
        //}
        //if (Input.GetMouseButton(0))//(Input.touchCount > 0)
        //{
        //    //touch = Input.GetTouch(0);
        //    deltaPos = Input.mousePosition - prevPos;
        //    //Debug.Log(deltaPos);
        //    //if (touch.phase == TouchPhase.Moved)
        //    //{
        //        transform.position = new Vector3(transform.position.x + deltaPos.x * instantiater.moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        //        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit.position.x + transform.localScale.x / 2, rightLimit.position.x - transform.localScale.x / 2), transform.position.y, transform.position.z);
        //    //}

        //}
        float xMove = 0;
        xMove += TrialManager.instance.controllerPad.x > 0.3f ? -1 : 0;
        xMove += TrialManager.instance.controllerPad.x < -0.3f ? 1 : 0;
        xMove *= instantiater.moveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x +xMove, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, rightLimit.position.x + transform.localScale.x / 2, leftLimit.position.x - transform.localScale.x / 2), transform.position.y, transform.position.z);

        if (TrialManager.instance.controllerTrigger>0.5f)//(touch.phase == TouchPhase.Ended)
            {
            Drop();
        }
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }

    }

    private void Drop()
    {
        instantiater.Drop();
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        GetComponent<Ball>().enabled = true;
        GetComponent<Ball>().isLocked = false;
        Destroy(GetComponent<instantiaterChild>());
    }

}
