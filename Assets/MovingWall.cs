using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public Vector3 dir;
    public Vector3 worldDir;
    public float speed;
    public Transform pair;
    public Transform[] onWall;
    // Start is called before the first frame update
    public void MoveWall()
    {
        if (TrialManager.instance.canMoveWalls)
        {
            worldDir = transform.TransformDirection(dir.x, dir.y, dir.z);
            transform.position += worldDir * speed * Time.deltaTime;
            //pair.position += worldDir * speed * Time.deltaTime;
            foreach (Transform t in onWall)
            {
                t.position += worldDir * speed * Time.deltaTime;
            }
        }
    }
}
