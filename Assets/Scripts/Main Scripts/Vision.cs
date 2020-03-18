using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {

    private bool visionObject;
    int zoomId;

    public GameObject BotaoQ;

    public float rayCastRange;
    RaycastHit hit;

    private Identificador Idem;

    /*
     * scene 1
     * 29 = segundo andar da guarita 
     * 30 = Ao olhar para a árvore caída em cima da oficina 
     * 31 = Ao olhar para o posto telegráfico 
     * 32 = Ao olhar para a porta entre os galpoes
     * 33 = Ao olhar para as portas destrancadas
     * 34 = ao ionteragir com uma porta trancada
     * 35 = ao entrar na serralheria
     * 
     * scene 2
     * 36 = Ao olhar para a área comum 
     * 37 = Ao olhar para o hospital de campanha 
     * 38 = Ao olhar para a água 
     * 39 = Ao olhar para o meio do cemitério 
     * 40 = Ao olhar para a fazenda de café 
     * 41 = Ao olhar para as arvores
     * 
     * scene 3
     * 42 = Ao olhar para as montanhas 
     * 43 = Ao olhar para o trilho nas pedras  
     * 44 = Ao olhar para o trilho na água 
     * 45 = Ao olhar para os vagões na água
     * 46 = Ao olhar para a água subindo
     * 47 = Ao olhar para o cupinzeiro
     */

    private void Update()
    {
        Debug.DrawRay(this.transform.position, Vector3.forward);
        if(Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, rayCastRange))
        {
            if(hit.collider.CompareTag(Globals.ZOOM_TAG))
            {
                if (!Globals.Aparecer_Texto)
                {
                    BotaoQ.SetActive(true);
                }
                visionObject = true;
                zoomId = hit.collider.gameObject.GetComponent<Identificador>().ZoomIdIdentificator();
                if(Input.GetKeyDown(KeyCode.Q))
                {
                    StartCoroutine("SelfDestruction");
                    BotaoQ.SetActive(false);
                }
                
            }

            if (hit.collider.CompareTag(Globals.UNTAGGED_TAG))
            {
                BotaoQ.SetActive(false);
                visionObject = false;
                zoomId = 0;
            }
        }
        else
        {
            BotaoQ.SetActive(false);
            visionObject = false;
            zoomId = 0;
        }
    }

    public bool SeeVisionObject()
    {
        return visionObject;
    }

    public int ZoomObjectId()
    {
        return zoomId;
    }

    public IEnumerator SelfDestruction()
    {
        yield return new WaitForSecondsRealtime(3);
        hit.collider.gameObject.GetComponent<Identificador>().SelfDestruction();
    }
}
