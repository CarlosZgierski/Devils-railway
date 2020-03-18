using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZone : MonoBehaviour {

    public int idItemNecessario;

    public bool ItemNecessario (int _item)
    {
        if (_item == idItemNecessario)
        {
            return true;
        }
        else return false;
    }
}
