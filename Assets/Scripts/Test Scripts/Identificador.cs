using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identificador : MonoBehaviour {

    public int VisionId;

    public int ZoomIdIdentificator()
    {
        return VisionId;
    }
    public void SelfDestruction()
    {
        Destroy(this.gameObject);
    }
}
