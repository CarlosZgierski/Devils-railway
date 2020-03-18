using UnityEngine;

public class Cipo : MonoBehaviour {

    public void DestroyCipo(int _idItem)
    {
        if(_idItem == 6)
        this.gameObject.SetActive(false);
    }
}
