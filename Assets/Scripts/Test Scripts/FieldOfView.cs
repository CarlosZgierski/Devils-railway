using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    float actualFoV;
    private bool zoom = true;

    public float fovInicial;
    public float zoomMaximo;

	Camera playerCamera;

    // Use this for initialization
    void Start () 
	{
        if (fovInicial == 0)
        {
            fovInicial = 70f;
        }
        if (zoomMaximo == 0)
        {
            zoomMaximo = fovInicial - 20f;
        }

        actualFoV = fovInicial;

        playerCamera = GetComponent<Camera> ();
        playerCamera.fieldOfView = actualFoV;

    }
	
	// Update is called once per frame
	void Update () 
	{
		playerCamera.fieldOfView = actualFoV;
        FovChanger();
        //PlayerInput();
    }

	void FovChanger()
	{
        if (Input.GetKeyDown(KeyCode.C))
        {
            zoom = !zoom;
        }
        if (!zoom)
        {
            if(playerCamera.fieldOfView > zoomMaximo)
            actualFoV = Mathf.Lerp(actualFoV, zoomMaximo, 10 * Time.deltaTime);
        } 
        else
        {
            if(playerCamera.fieldOfView < fovInicial)
            actualFoV = Mathf.Lerp(actualFoV, fovInicial, 10 * Time.deltaTime);
        }
	}

    void OnGUI()
    {
        //Set up the maximum and minimum values the Slider can return (you can change these)
        float max, min;
        max = 150.0f;
        min = 20.0f;
        //This Slider changes the field of view of the Camera between the minimum and maximum values
        actualFoV = GUI.HorizontalSlider(new Rect(20, 20, 100, 40), actualFoV, min, max);
    }
}
