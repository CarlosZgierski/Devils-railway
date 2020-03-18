using UnityEngine;

public class FeetTest : MonoBehaviour {

    bool chao;

    private void OnTriggerStay(Collider other)
    {
        chao = true;
    }

    private void OnTriggerExit(Collider other)
    {
        chao = false;
    }

    public bool TouchedGround()
    {
        return chao;
    }
}
