using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    public Transform pair;
    public Transform[] onWall;
    // Start is called before the first frame update
    public void MoveWall()
    {
        transform.position += dir * speed * Time.deltaTime;
        pair.position += dir * speed * Time.deltaTime;
        foreach (Transform t in onWall)
        {
            t.position += dir * speed * Time.deltaTime;
        }
        Debug.Log("walls moving");
    }
}
