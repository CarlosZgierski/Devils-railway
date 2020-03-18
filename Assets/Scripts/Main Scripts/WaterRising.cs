using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterRising : MonoBehaviour {

    [SerializeField] private float waterRisingSpeed;
    [SerializeField] private float timeBetweenRise = 1f; //in seconds

    public float waterMaxHeight = 10000000000;

    [SerializeField] private Transform respawnPosition;
    [SerializeField] private GameObject playerRef;
    [SerializeField] private GameObject waterTriggerReference;

    private Vector3 initialWaterPosition;

    void Start()
    {
        initialWaterPosition = this.transform.position;
    }

    IEnumerator RiseWater()
    {
        while(this.transform.position.y < waterMaxHeight)
        {
            yield return new WaitForSeconds(timeBetweenRise);
            float newLevel = this.transform.position.y + waterRisingSpeed;
            this.transform.position = new Vector3(this.transform.position.x,newLevel, this.transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Globals.PLAYER_CHEST_TAG))
        {
            playerRef.transform.position = respawnPosition.position;
            this.transform.position = initialWaterPosition;
            waterTriggerReference.GetComponent<WaterTrigger>().ReactivateTrigger();
        }
    }
}
