using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ObjectTest : MonoBehaviour {

    public int objectNumber;
    public AudioSource Sound;

    public void SelfDestruct()
    {
        Destroy(this.gameObject);
        Sound.Play();
    }
}
