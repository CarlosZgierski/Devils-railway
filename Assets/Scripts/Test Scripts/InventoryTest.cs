using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour {

    /*
     * id 1 = madeira
     * id 2 = cu
     * id 3 = fruz
     * */


    /* Basics do codigo
     * pegar o item
     * colocar no inventario
     * apagar o item
     * array com todas as imagens possiveis (global maybe)
     * array do inverntario, provavelmente uma função que permita uma interação, ou deixar isso em outro script
     * TALVEZ alguns poucos itens permaneçam entre os loads
    *****/


    //ITEMS NÃO PODEM SER ZERO (0), ZERO É VALOR VAZIO

    public Sprite [] possiveisItems = new Sprite[5];

    public GameObject[] posiHuds= new GameObject[5];


}
