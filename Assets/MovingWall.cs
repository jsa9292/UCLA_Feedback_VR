using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    public Transform pair;
    // Start is called before the first frame update
    public void MoveWall()
    {
        transform.position += dir * speed * Time.deltaTime;
        pair.position += dir * speed * Time.deltaTime;
        Debug.Log("walls moving");
    }
}
