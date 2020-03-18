using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionTest : MonoBehaviour {

    private bool visionObject;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(GlobalsTest.ZOOM_TAG))
        {
            visionObject = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GlobalsTest.ZOOM_TAG))
        {
            visionObject = false;
        }
    }

    public bool SeeVisionObject()
    {
        return visionObject;
    }
}
