using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggedSound : MonoBehaviour {

    public AudioSource Sound;

    void OnTriggerEnter(Collider other)
    {
        Sound.Play();
        Destroy(this.gameObject);
    }
}
