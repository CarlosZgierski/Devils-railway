using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class InteractiveObject : MonoBehaviour {

    public bool telegrama;
    public int telegramaVolta;
    private bool telegramaDuasVezes = true;

    public bool colecionavel;
    public bool documento;

    public int objectNumber;

    public void SelfDestruct(Inventory _inventario) 
    {
        if (!telegrama || !documento || !colecionavel)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (!telegramaDuasVezes)
            {
                _inventario.getSpriteOnHud(telegramaVolta);
                Destroy(this.gameObject);
            }
            else if(telegramaDuasVezes)
            {
                _inventario.getSpriteOnHud(objectNumber);

                telegramaDuasVezes = false;
            }
        }
    }

    public void SelfDestruct()
    {
            Destroy(this.gameObject);
    }

}
