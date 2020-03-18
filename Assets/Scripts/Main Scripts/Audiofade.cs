using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Audiofade : MonoBehaviour {
    public AudioMixerGroup group;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
      group.audioMixer.SetFloat("Cutoff", 3000);
    }
    private void OnTriggerExit(Collider other)
    {
        group.audioMixer.SetFloat("Cutoff", 6652);
    }
}
