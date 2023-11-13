using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnCollision : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
