using UnityEngine;

public class StairZone : MonoBehaviour {

    public GameObject ladder;

    private void Start()
    {
        if(ladder == null)
        {
            ladder = GetComponentInChildren<GameObject>();
        }
    }

    public bool ActivateLadder(int _idItem)
    {
        if(_idItem == 2)
        {
            ladder.gameObject.SetActive(true);
                return false;
        }
        else
        {
            ladder.gameObject.SetActive(false);
                return true;
        }
    }
    
}
