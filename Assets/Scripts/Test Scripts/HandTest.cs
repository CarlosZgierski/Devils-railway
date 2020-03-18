using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandTest : MonoBehaviour
{
    private GameObject item;

    private void OnTriggerStay(Collider obj)
    {
        if (obj.CompareTag(GlobalsTest.OBJECTS_TAG) || obj.CompareTag(GlobalsTest.INTERACTION_TAG) || obj.CompareTag(GlobalsTest.CIPO_TAG) || obj.CompareTag(GlobalsTest.DOOR_TAG))
        {
            item = obj.gameObject;
        }
        if (obj.CompareTag(Globals.LEVEL01_LEVEL02) && Input.GetKeyDown("e"))
        {
            SceneManager.LoadScene(2);
        }

        if (obj.CompareTag(Globals.LEVEL02_LEVEL03) && Input.GetKeyDown("e"))
        {
            SceneManager.LoadScene(3);
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        if (obj.CompareTag(GlobalsTest.OBJECTS_TAG) || obj.CompareTag(GlobalsTest.INTERACTION_TAG) || obj.CompareTag(GlobalsTest.CIPO_TAG) || obj.CompareTag(GlobalsTest.DOOR_TAG))
        {
            item = null;
        }
    }

    public GameObject SelectedObject()
    {
        if (item != null)
        {
            return item;
        }
        else return null;
    }

}
