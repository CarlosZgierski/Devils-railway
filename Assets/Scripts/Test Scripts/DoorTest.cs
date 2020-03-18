using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : MonoBehaviour {

    public bool doorOpen;
    public float openedDoorRotation;
    public float rotationVel = 1f;

    private float rotationY;
    private float initialRotationY;
    private float t;

    private float valor;

    private void Start()
    {
        if(openedDoorRotation == 0)
        {
            openedDoorRotation = this.transform.rotation.y - 90;
        }
        rotationY = this.transform.rotation.y;
        initialRotationY = this.transform.rotation.y;
    }

    private void Update()
    {

        if(doorOpen)
        {
            rotationY = Mathf.Lerp(rotationY, openedDoorRotation, Time.deltaTime * rotationVel);

            Vector3 rotationTarget = new Vector3(0, rotationY, 0);

            this.transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }
        else
        {
            rotationY = Mathf.Lerp(rotationY, initialRotationY, Time.deltaTime * rotationVel);

            Vector3 rotationTarget = new Vector3(0, rotationY, 0);

            this.transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }
    }

    public void DoorInteraction()
    {
        doorOpen = !doorOpen;
    }
}
