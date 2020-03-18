using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZoneTest : MonoBehaviour {

    public int idItemNecessario;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool ItemNecessario (int _item)
    {
        if (_item == idItemNecessario)
        {
            return true;
        }
        else return false;
    }
}
