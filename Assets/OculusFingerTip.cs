using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusFingerTip : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Wall") {
            other.GetComponent<MovingWall>().MoveWall();
        }
    }
}
