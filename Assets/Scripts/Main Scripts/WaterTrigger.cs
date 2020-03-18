using UnityEngine;

public class WaterTrigger : MonoBehaviour {

    [SerializeField] private GameObject waterReference;

    private Collider colRef;

    private void Start()
    {
        colRef = this.gameObject.GetComponent<Collider>();
    }

    public void ReactivateTrigger()
    {
        waterReference.GetComponent<WaterRising>().StopAllCoroutines();
        colRef.enabled = enabled;
    }

    private void OnTriggerEnter(Collider _col)
    {
        if(_col.CompareTag(Globals.PLAYER_TAG))
        {
            waterReference.GetComponent<WaterRising>().StartCoroutine("RiseWater");
            colRef.enabled = false;
        }
    }
}
