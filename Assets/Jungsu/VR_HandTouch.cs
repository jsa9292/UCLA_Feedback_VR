using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HTC.UnityPlugin.ColliderEvent.ColliderAxisEventData;

public class VR_HandTouch : MonoBehaviour
{
    public OpenBook ob;
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            other.transform.GetComponent<EditorButton>().onButtonPressedChangeColor();
        }
        catch
        {
            Debug.Log("Not a button: " + other.gameObject.name);
            try {
                //ob = other.transform.GetComponent<OpenBook>();
                ob.onTouchEntered(transform.position);
            }
            catch {
                Debug.Log("There shouldnt be trigger here" + other.gameObject.name);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Wall") {
            MovingWall mw = other.GetComponent<MovingWall>();
            mw.MoveWall();
            return;
        } 
        ob.onTouchMoved(transform.position);

    }
    private void OnTriggerExit(Collider other)
    {
        try
        {
            other.GetComponent<EditorButton>().onButtonReleasedChangeColor();
        }
        catch
        {
            Debug.Log("Not a button: " + other.gameObject.name);
        }
    }
}
