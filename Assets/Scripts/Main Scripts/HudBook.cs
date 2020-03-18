using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudBook : MonoBehaviour {

    public string[] centralButtonTexts;
    public GameObject [] centralZone;
    public Button [] centralButtonsReference = new Button [14];
    public Text[] centralButtonTextReference = new Text[14];
    /* id dos butões
    * 0-2   ->Cartas
    * 3-7   ->Documentos
    * 8-13  ->Telegramas
    */

    public Sprite[] colectableSprites;

    public string nullButton = "????";
    public Image rightImageZone;

    public GameObject playerReference;
    private Player player;

    private bool[] acquiredItens = new bool [40];
    private bool[] enabledButtons = new bool[21];

    private int currentCentraArea;

    private void OnEnable()
    {
        player = playerReference.GetComponent<Player>();
        WhichCollectablesAreEnabled();
    }

    private void Start()
    {
        UpdateCentralButtons();
        if(playerReference == null)
        player = playerReference.GetComponent<Player>();

        acquiredItens = player.acquiredColectables;

        for(int x = 0; x< centralButtonTextReference.Length;x++)
        {
            centralButtonTextReference[x] = centralButtonsReference[x].GetComponentInChildren<Text>();
        }
        WhichCollectablesAreEnabled();
    }

    private void Update()
    {
        UpdateCentralButtons();
    }

    #region Public Functions

    public void ActivateCentralCollum(int _buttom)
    {
        for (int x = 0; x < centralZone.Length; x++)
        {
            if(x!= _buttom)
            {
                centralZone[x].SetActive(false);
            }
            else
            {
                centralZone[x].SetActive(true);
                currentCentraArea = x;
            }
        }

        //rightImageZone
    }

    public void UpdateCentralButtons()
    {
        if(currentCentraArea == 0)
        {
            for(int x = 0; x<3; x ++)
            {
                if(enabledButtons[x])
                    centralButtonTextReference[x].text = centralButtonTexts[x];
                else
                    centralButtonTextReference[x].text = nullButton;
            }
        }
        else if(currentCentraArea == 1)
        {
            for (int y = 3; y < 8; y++)
            {
                if (enabledButtons[y])
                    centralButtonTextReference[y].text = centralButtonTexts[y];
                else
                    centralButtonTextReference[y].text = nullButton;
            }
        }
        else if (currentCentraArea == 2)
        {
            for (int z =8; z < 14; z++)
            {
                if (enabledButtons[z])
                    centralButtonTextReference[z].text = centralButtonTexts[z];
                else
                    centralButtonTextReference[z].text = nullButton;
            }
        }
        else if(currentCentraArea == 3)
        {
            for (int v = 14; v < 21; v++)
            {
                if (enabledButtons[v])
                    centralButtonTextReference[v].text = centralButtonTexts[v];
                else
                    centralButtonTextReference[v].text = nullButton;
            }
        }
    }

    public void ShowImage (int _idImage)
    {
        if(centralButtonTextReference[_idImage].text != nullButton)
            rightImageZone.sprite = colectableSprites[_idImage];
    }

    public void CloseTab()
    {
        this.gameObject.SetActive(false);
    }

    #endregion

    #region Private Functions

    private void WhichCollectablesAreEnabled()
    {
        //Reorganizar o array de 30 para o de 14 
        for(int x = 0; x< acquiredItens.Length; x++)
        {
            if(x == 10) // documento 1
            {
                if(acquiredItens[x])
                {
                    enabledButtons[3] = true;
                }
            }
            else if (x == 12) //documento 2
            {
                if (acquiredItens[x])
                {
                    enabledButtons[4] = true;
                }
            }
            else if (x == 13) //documento 3
            {
                if (acquiredItens[x])
                {
                    enabledButtons[5] = true;
                }
            }
            else if (x == 14) //documento 4
            {
                if (acquiredItens[x])
                {
                    enabledButtons[6] = true;
                }
            }
            else if (x == 15) //documento 5
            {
                if (acquiredItens[x])
                {
                    enabledButtons[7] = true;
                }
            }
            else if (x == 20) //carta 1
            {
                if (acquiredItens[x])
                {
                    enabledButtons[0] = true;
                }
            }
            else if (x == 21) //carta 2
            {
                if (acquiredItens[x])
                {
                    enabledButtons[1] = true;
                }
            }
            else if (x == 28) //carta 3
            {
                if (acquiredItens[x])
                {
                    enabledButtons[2] = true;
                }
            }
            else if (x == 22) //ida 1
            {
                if (acquiredItens[x])
                {
                    enabledButtons[8] = true;
                }
            }
            else if (x == 23) //ida 2
            {
                if (acquiredItens[x])
                {
                    enabledButtons[9] = true;
                }
            }
            else if (x == 24) //ida 3
            {
                if (acquiredItens[x])
                {
                    enabledButtons[10] = true;
                }
            }
            else if (x == 25) //volta 1
            {
                if (acquiredItens[x])
                {
                    enabledButtons[11] = true;
                }
            }
            else if (x == 26) //volta 2
            {
                if (acquiredItens[x])
                {
                    enabledButtons[12] = true;
                }
            }
            else if (x == 27) //volta 3
            {
                if (acquiredItens[x])
                {
                    enabledButtons[13] = true;
                }
            }
            else if (x == 30) //Colecionavel 1
            {
                if (acquiredItens[x])
                {
                    enabledButtons[14] = true;
                }
            }
            else if (x == 31) //Colecionavel 2
            {
                if (acquiredItens[x])
                {
                    enabledButtons[15] = true;
                }
            }
            else if (x == 32) //Colecionavel 3
            {
                if (acquiredItens[x])
                {
                    enabledButtons[16] = true;
                }
            }
            else if (x == 33) //Colecionavel 4
            {
                if (acquiredItens[x])
                {
                    enabledButtons[17] = true;
                }
            }
            else if (x == 34) //Colecionavel 5
            {
                if (acquiredItens[x])
                {
                    enabledButtons[18] = true;
                }
            }
            else if (x == 35) //Colecionavel 6
            {
                if (acquiredItens[x])
                {
                    enabledButtons[19] = true;
                }
            }
            else if (x == 36) //Colecionavel 7
            {
                if (acquiredItens[x])
                {
                    enabledButtons[20] = true;
                }
            }
        }
    }

    #endregion
}
