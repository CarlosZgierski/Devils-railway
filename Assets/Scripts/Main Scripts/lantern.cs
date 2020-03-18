using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lantern : MonoBehaviour {

    private Light lt;
    int aleatorio;
    float tempo;
    float mudanca;
    bool desligar;

    // Use this for initialization
    void Start ()
    {
        mudanca = 0;
        lt = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        tempo += 1 * Time.deltaTime;
        if(tempo >= 3)
        {
            aleatorio = Random.Range(0, 100);
            if (aleatorio <= 80 && aleatorio >= 60)
            {
                falha();
            }
            else if (aleatorio <= 20 && aleatorio >= 0)
            {
                falha();
            }
            else
            {
              certo();
            }
            
        }
      

        if(Input.GetKeyDown(KeyCode.F))
        {
            desligar = !desligar;
        }

        if(desligar)
        {
            lt.intensity =0;
        }
        else
        {
            lt.intensity = 1.7f;
        }

    }

    void falha()
    {
        mudanca+=1*Time.deltaTime;
        if (mudanca >= 0 && mudanca <= 2f)
            lt.intensity = 0.40f;
        else if (mudanca >= 2f && mudanca <= 4f)
            lt.intensity = 0.85f;
        else if (mudanca >= 4f && mudanca <= 6f)
            lt.intensity = 0.0f;
        else if (mudanca >= 6f && mudanca <= 8) 
        {
            lt.intensity = 1.7f;
            mudanca = 0;
            tempo = 0;
            
        }
    }

    void certo()
    {
        mudanca = 0;
        lt.intensity = 1.7f;
        tempo = 0;
    }
}
