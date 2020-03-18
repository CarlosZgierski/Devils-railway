using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelegraficPoint : MonoBehaviour {

    public int objectId;
    public int seconfObjectId;

    [SerializeField] private Sprite telegramaIda;
    [SerializeField] private Sprite telegramaVolta;
    [SerializeField] private GameObject hudCenterPoint;

    [SerializeField] private GameObject playerRef;

    private Player playerScript;

    private Image rendererRef;
    private Collider colRef;

    private bool firstClick = true;
    private bool lastClick = false;

	void Start ()
    {
        rendererRef = hudCenterPoint.GetComponent<Image>();
        colRef = this.GetComponent<Collider>();

        playerScript = playerRef.GetComponent<Player>();
	}
	
    public void ShowOnHUd()
    {
        if(firstClick)
        {
            Player.contadorC+=1;
            hudCenterPoint.SetActive(true);
            rendererRef.sprite = telegramaIda;
            firstClick = false;
        }
        else if(!firstClick && !lastClick)
        {
            Player.contadorC += 1;
            hudCenterPoint.SetActive(true);
            rendererRef.sprite = telegramaVolta;
            lastClick = true;
        }
        else if(lastClick)
        {
            colRef.enabled = false;
            RemoveFromHud();
            playerScript.itemOnHud = false;
            Player.contadorC = 0;
        }
    }

    public void RemoveFromHud()
    {
        hudCenterPoint.SetActive(false);
    }
}
