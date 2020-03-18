using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour {

    public Animator anim;

    public GameObject Cam1;
    public GameObject Cam2;
    public GameObject toolbar;


    public void PlayAnim()
    {
        anim.SetBool("Verdadeiro", true);
    }

    public void ActiveCam()
    {
        Cam2.SetActive(true);
        toolbar.SetActive(true);
        Cam1.SetActive(false);
    }
   
}
