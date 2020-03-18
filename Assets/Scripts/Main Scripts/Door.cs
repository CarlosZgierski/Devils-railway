using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Door : MonoBehaviour {

    public bool doorClosed = false;
    public bool lockedDoor;
    public bool initialRotationNegative;
    public int idNeededKey;
    public bool rollingDoor;
    public bool useX;

    public float openedDoorRotation;
    public float openedDoorPosition;
    public float rotationVel = 1f;

    private AudioSource audioPlayer;
    private float rotationY;
    private float slidePos;
    private float initialRotationY;
    private float initialPos;

    private void Start()
    {
        if(initialRotationNegative)
        {
            rotationY = (transform.localRotation.eulerAngles.y - 360) *-1;
            initialRotationY = (transform.localRotation.eulerAngles.y -360) * -1;
        }
        else
        {
            rotationY = transform.localRotation.eulerAngles.y;
            initialRotationY = transform.localRotation.eulerAngles.y;
        }

        //rolling door start functions
        if (useX)
        {
            slidePos = this.transform.localPosition.x;
            initialPos = this.transform.localPosition.x;
        }
        else
        {
            slidePos = this.transform.localPosition.z;
            initialPos = this.transform.localPosition.z;
        }

        audioPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (doorClosed)
        {
            if (!rollingDoor)
            {
                rotationY = Mathf.Lerp(rotationY, initialRotationY, Time.deltaTime * rotationVel);

                Vector3 rotationTarget = new Vector3(0, rotationY, 0);

                this.transform.rotation = Quaternion.Euler(0, rotationY, 0);
            }
            else
            {
                slidePos = Mathf.Lerp(slidePos, initialPos, Time.deltaTime);

                if (useX)
                {
                    Vector3 positionTarget = new Vector3(slidePos, this.transform.localPosition.y, this.transform.localPosition.z);
                    this.transform.localPosition = positionTarget;
                }
                else
                {
                    Vector3 positionTarget = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, slidePos);
                    this.transform.localPosition = positionTarget;
                }
            }
        }
        else
        {
            if (!rollingDoor)
            {
                rotationY = Mathf.Lerp(rotationY, openedDoorRotation, Time.deltaTime * rotationVel);

                Vector3 rotationTarget = new Vector3(0, rotationY, 0);

                this.transform.rotation = Quaternion.Euler(0, rotationY, 0);
            }
            else
            {
                slidePos = Mathf.Lerp(slidePos, openedDoorPosition, Time.deltaTime);

                if (useX)
                {
                    Vector3 positionTarget = new Vector3(slidePos, this.transform.localPosition.y, this.transform.localPosition.z);
                    this.transform.localPosition = positionTarget;
                }
                else
                {
                    Vector3 positionTarget = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, slidePos);
                    this.transform.localPosition = positionTarget;
                }
            }
        }
    }

    public void DoorInteraction()
    {
         doorClosed = !doorClosed;
         audioPlayer.Play();

    }

    public bool LockedDoorInteraction(int _idItem)
    {
        if (_idItem == idNeededKey)
        {
            doorClosed = !doorClosed;
            lockedDoor = false;
            return true;
        }
        else return false;
    }
}
