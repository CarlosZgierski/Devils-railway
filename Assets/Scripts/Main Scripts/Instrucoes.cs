using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrucoes : MonoBehaviour {

    public GameObject instru;
    public GameObject menu;

    public void Menu()
    {
        menu.SetActive(true);
        instru.SetActive(false);
    }
}
