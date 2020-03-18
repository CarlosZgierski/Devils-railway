using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    /*
     * items de 1 a 9
     * id 1 = lanterna
     * id 2 = escada
     * id 3 = chave posto
     * id 4 = chave serralheria
     * id 5 = chave "vagão x"
     * id 6 = facão
     * id 7 = Chave vagão
     * 
     * 
     * documentos de 10 a 19
     * id 10 = documento 00 (guarita) paciente 1
     * id 11 = documento 01 (Inexistente)
     * id 12 = documento 02 (Vagão X)paciente 2
     * id 13 = documento 03 (Documento Hospital 2.1)paciente 3
     * id 14 = documento 04 (Documento Hospital 2.2)paciente 4
     * id 15 = documento 05 (Documento Hospital 3)paciente5
     * 
     * colecionaveis de 20 a 29
     * id 20 = Carta 00 (Vagão 2) Arnaldo
     * id 21 = Carta 01 (caixa de força 3) Diogenes
     * id 22 = Telegrama IDA 00 (Ida level 1)
     * id 23 = Telegrama IDA 01 (Ida level 2)
     * id 24 = Telegrana IDA 02 (Ida level 3)
     * id 25 = Telegrana VOLTA 03 (volta level 1)Benedito para Francisco 1
     * id 26 = Telegrana VOLTA 04 (volta level 2)Benedito para Francisco 2
     * id 27 = Telegrana VOLTA 05 (volta level 3)Benedito para Francisco 3 ---- End game pq sei la
     * id 28 = Carta sei la (Posto Telegrafico 3) Obito
     * id 29 = Documento medico, Obituario
     * 
     * //malditos colecionaveis
     * id 30 = colecionavel 1
     * id 31 = colecionavel 2
     * id 32 = colecionavel 3
     * id 33 = colecionavel 4
     * id 34 = colecionavel 5
     * id 35 = colecionavel 6
     * id 36 = colecionavel 7
     */

    //ITEMS NÃO PODEM SER ZERO (0), ZERO É VALOR VAZIO

    public Sprite [] possiveisItems = new Sprite[30];

    public GameObject[] posiHuds= new GameObject[30];

    private Player playerRef;

    public GameObject hudCenterSpot;

    private void Start()
    {
        playerRef = this.gameObject.GetComponent<Player>();
    }

    public void getSpriteOnHud(int idItem)
    {
        hudCenterSpot.GetComponent<SpriteRenderer>().sprite = possiveisItems[idItem];
        hudCenterSpot.SetActive(true);
        playerRef.itemOnHud = true;
    }


    public void RemoveSpriteOnHud()
    {
        hudCenterSpot.GetComponent<SpriteRenderer>().sprite = null;
        hudCenterSpot.SetActive(false);

        playerRef.itemOnHud = false;
    }

}
