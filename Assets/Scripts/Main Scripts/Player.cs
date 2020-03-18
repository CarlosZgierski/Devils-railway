using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    Rigidbody player;

    //pause 
    public GameObject T_pause;

    //Variavel item hud
    public bool itemOnHud;

    //Modo do cursor na tela
    public CursorLockMode wantedMode;

    //variaveis de velocidade da camera e velocidade do jogador
    float vel = 100f;
    float sensitivity = 2f;
    float jumpHeight = 0.2f;

    //Vetores da movimentação do jogador no chão e nas escadas
    float moveFB;
    float moveLR;
    float moveLadder;
    bool onLadder;

    //Vetores do mouse do jogador
    float lookZ;
    float lookX;

    //Camera do jogador
    public Camera playerCamera;

    //Script da mão jogador
    public GameObject hand;
    Hand handScript;

    //Variaveis para saber qual o item atual na mão do jogador
    GameObject item;
    int keyAtual;
    int ultimoItem;
    int ultimaKey;

    //Variaveis usados para o inventario e checagem de items do inventario
    private int[] inventario = new int [4];
    Inventory inventarioPossivel; //Possiveis itens que o jogador pode pegar durante o jogo
    [SerializeField] private Sprite nullHudPosition;

    private Image centerOfHudImage;
    [SerializeField] private GameObject CenterOfHud;

    //Variaveis para que o jogador faça somente um pulo
    //public Feet feet;
    bool isGrounded;

    //Variaveis para o zoom da camera e do Field of View do jogador
    float actualFoV;
    private bool zoom = true;
    public float fovInicial;
    public float zoomMaximo;

    //variaveis para a escrita da visao
    int contador;
    public GameObject[] interactionTexts;

    //Variaveis para o zoom em certos objetos
    bool canZoom;
    public static bool olhando;
    public GameObject vision;
    Vision visionScript;

    //arrays para o armazenamento das bool de item colecionaveis e documentos pegos pelo jogador
    public bool[] acquiredColectables = new bool [40];

    //ajudar na coroutine
    private int valor;
    private int valor2;
    public static int contadorC;

    //Referebcia do FpsController
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;

    //ativar jogo pausado
    public static bool pause;

    //variavel para saber se era um item carta ou telegrafo
    private bool carta;
    private bool ultimaMerda;

    //Variaveis de Cheat
    [SerializeField] private bool cheatMode;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //check para os fovs do jogador
        if (fovInicial == 0)
        {
            fovInicial = 70f;
        }
        if (zoomMaximo == 0)
        {
            zoomMaximo = fovInicial - 20f;
        }
        actualFoV = fovInicial;

        //estado inicial do cursor na tela
        Cursor.lockState = wantedMode;

        player = GetComponent<Rigidbody>();
        inventarioPossivel = GetComponent<Inventory>();

        //colocar a Fov Inicial
        playerCamera.fieldOfView = actualFoV;
        visionScript = vision.GetComponent<Vision>();

        fpsController = this.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

        //pegar as referencias necessarias nas children do player
        /*if (feet == null)
        {
            feet = GetComponentInChildren<Feet>();
        }*/
        if(playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }
        if(handScript == null)
        {
            handScript = GetComponentInChildren<Hand>();
        }

        centerOfHudImage = CenterOfHud.GetComponent<Image>();

        if(cheatMode)
        {
            for (int x =0; x < acquiredColectables.Length;x++)
            {
                acquiredColectables[x] = true;
            }

            inventario[0] = 2;
            inventario[1] = 6;
        }
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        inventario = PlayerPrefsX.GetIntArray("inventario");
        if(inventario.Length < 4)
        {
            inventario = new int[4];
        }
        acquiredColectables = PlayerPrefsX.GetBoolArray("itens");
        if(acquiredColectables.Length < 40)
        {
            acquiredColectables = new bool[40];
        }
    }

    void Update()
    {
        playerCamera.fieldOfView = actualFoV;
        if (!Player.pause)
        {
            PlayerInteraction();
            PlayerInventory();
            PlayerInventoryHud();
            PlayerJump();
            FovChanger();
        }
        PlayerPause();

        print("ultimo item ID :" + ultimoItem);

    }

    void PlayerMovement()
    {
        moveFB = Input.GetAxis("Horizontal") * vel ;
        moveLR = Input.GetAxis("Vertical") * vel ;

        moveLadder = Input.GetAxis("Vertical") * vel / 10;

        if(Input.GetAxis("Horizontal") == 0)
        {
            moveFB -= moveFB;
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            moveFB -= moveLR;
        }

        lookX -= Input.GetAxis("Mouse Y") * sensitivity;
        lookZ = Input.GetAxis("Mouse X");
        lookX = Mathf.Clamp(lookX, -70, 70);


        Vector3 movement = new Vector3(moveFB, 0, moveLR);
        movement = this.transform.rotation * movement;

        this.transform.Rotate(0, lookZ * sensitivity, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(lookX, 0, 0);


        //check para se o jogador estiver na escada ou no chão
        if (!onLadder)
        {
            player.velocity += (movement * Time.deltaTime);
        }
        else
        {
            Vector3 latterM = new Vector3(0, moveLadder, 0);
            this.transform.position += latterM * Time.deltaTime;
        }

    }

    void PlayerJump()
    {
        if (onLadder)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.velocity += new Vector3(-jumpHeight, 0, 0);
            }
            if(Input.GetKey(KeyCode.W))
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y +0.5f, this.transform.position.z);
            }
        }

    }

    void PlayerVelocityCap()
    {
        if (player.velocity.magnitude > 10)
        {
            player.velocity = player.velocity.normalized * 10;
        }
    }

    void PlayerInteraction()
    {
        if (!Globals.Aparecer_Texto)
        {
            if (Input.GetKeyDown("e"))
            {
                item = handScript.SelectedObject();
                if (itemOnHud)
                {
                    CenterOfHud.SetActive(false);
                    itemOnHud = false;

                    #region LegacyCode
                    /*if (item.CompareTag(Globals.TELEGRAPIC_TAG))
                    {
                        if(!itemOnHud)
                        {
                            if(contadorC >=2)
                            {
                                Globals.Aparecer_Texto = true;
                                StartCoroutine("Apagao3");
                            }
                        }
                    }
                    else
                    {
                        Globals.Aparecer_Texto = true;
                        StartCoroutine("Apagao");
                    }*/
                    #endregion

                    
                    if (!carta)
                    {
                        if (contadorC >= 2)
                        {
                            if (!ultimaMerda)
                            {
                                Globals.Aparecer_Texto = true;
                                StartCoroutine(Apagao3(carta));
                            }
                            else
                            {
                                Globals.Aparecer_Texto = true;
                                StartCoroutine(ApagaoFinal());
                            }
                        }
                    }
                    else
                    {
                        Globals.Aparecer_Texto = true;
                        StartCoroutine(Apagao3(carta));
                    }
                }

                if (item.CompareTag(Globals.OBJECTS_TAG))
                {
                    if (!item.GetComponent<InteractiveObject>().colecionavel || !item.GetComponent<InteractiveObject>().documento)
                    {
                        Globals.Aparecer_Texto = true;

                        for (int x = 0; x < inventario.Length; x++)
                        {
                            if (inventario[x] == 0)
                            {
                                inventario[x] = item.GetComponent<InteractiveObject>().objectNumber;
                                valor = item.GetComponent<InteractiveObject>().objectNumber;
                                StartCoroutine("Apagao");
                                item.GetComponent<InteractiveObject>().SelfDestruct(inventarioPossivel);
                                valor = inventario[x];
                                break;
                            }
                        }
                    }
                    else
                    {
                        //coloca uma bool true para os objectos que o jogador ja pegoue  deleta o objeto
                        centerOfHudImage.sprite = inventarioPossivel.possiveisItems[item.GetComponent<InteractiveObject>().objectNumber];
                        CenterOfHud.SetActive(true);
                        itemOnHud = true;
                        acquiredColectables[item.GetComponent<InteractiveObject>().objectNumber] = true;
                        valor = item.GetComponent<InteractiveObject>().objectNumber;
                        carta = true;
                        item.GetComponent<InteractiveObject>().SelfDestruct();
                    }
                }
                else if (item.CompareTag(Globals.INTERACTION_TAG))
                {
                    if (item.GetComponent<InteractionZone>().ItemNecessario(ultimoItem))
                    {
                        inventario[ultimaKey] = 0;
                        ultimoItem = 0;
                        item.SetActive(false);
                    }
                }
                else if (item.CompareTag(Globals.CIPO_TAG))
                {
                    item.GetComponent<Cipo>().DestroyCipo(ultimoItem);
                }
                else if (item.CompareTag(Globals.DOOR_TAG))
                {
                    if (!item.GetComponent<Door>().lockedDoor)
                    {
                        item.GetComponent<Door>().DoorInteraction();
                    }
                    else
                    {
                        if (item.GetComponent<Door>().LockedDoorInteraction(ultimoItem))
                        {
                            inventario[ultimaKey] = 0;
                            ultimoItem = 0;
                        }
                        else
                        {
                            Globals.Aparecer_Texto = true;
                            StartCoroutine("PortaTrancada");
                        }
                    }
                }
                else if (item.CompareTag(Globals.STAIR_ZONE_TAG))
                {
                    Transform childrenStair = item.transform.GetChild(0);
                    if (childrenStair.gameObject.activeInHierarchy == true && item.GetComponent<StairZone>().ActivateLadder(ultimoItem))
                    {
                        for (int x = 0; x < inventario.Length; x++)
                        {
                            if (inventario[x] == 0)
                            {
                                inventario[x] = 2;
                                break;
                            }
                        }
                    }
                    else if (!item.GetComponent<StairZone>().ActivateLadder(ultimoItem))
                    {
                        inventario[ultimaKey] = 0;
                        ultimoItem = 0;
                    }
                }
                else if (item.CompareTag(Globals.TELEGRAPIC_TAG))
                {
                    acquiredColectables[item.GetComponent<TelegraficPoint>().objectId] = true;
                    acquiredColectables[item.GetComponent<TelegraficPoint>().seconfObjectId] = true;
                    valor2 = item.GetComponent<TelegraficPoint>().seconfObjectId;
                    item.GetComponent<TelegraficPoint>().ShowOnHUd();
                    if(item.GetComponent<TelegraficPoint>().seconfObjectId == 27)
                    {
                        ultimaMerda = true;
                    }
                    itemOnHud = true;
                    carta = false;
                }
            }

            canZoom = visionScript.SeeVisionObject();
            if (!canZoom)
            {
                zoom = true;
            }

            if (Input.GetKeyDown(KeyCode.Q) && canZoom == true)
            {
                Globals.Aparecer_Texto = true;
                olhando = true;
                contador = visionScript.ZoomObjectId();
                StartCoroutine("Apagao2");
                zoom = !zoom;
            }
        }
    }

    void PlayerInventory()
    {
        
        if(int.TryParse(Input.inputString, out keyAtual))
        {
            if (keyAtual <= inventario.Length)
            {
                print("slot " + keyAtual + " : " + inventario[keyAtual - 1]);
            }
        }
        if (keyAtual != 0)
        {
            ultimoItem = inventario[keyAtual - 1];
            ultimaKey = keyAtual - 1;
        }
    }

    void PlayerInventoryHud()
    {
        for (int x = 0; x < inventario.Length; x++)
        {
            int iD = inventario[x];
            Sprite item;

            //Id = 0 é sempre item nulo
            if (iD == 0)
            {
                inventarioPossivel.posiHuds[x].GetComponent<Image>().sprite = nullHudPosition;
                break;
            }
            else
            {
                item = inventarioPossivel.possiveisItems[iD];
                inventarioPossivel.posiHuds[x].GetComponent<Image>().sprite = item;
            }

        }
    }

    void FovChanger()
    {
        if (!zoom)
        {
            if (playerCamera.fieldOfView > zoomMaximo)
                actualFoV = Mathf.Lerp(actualFoV, zoomMaximo, 10 * Time.deltaTime);
        }
        else
        {
            if (playerCamera.fieldOfView < fovInicial)
                actualFoV = Mathf.Lerp(actualFoV, fovInicial, 10 * Time.deltaTime);
        }
    }

    void GravityCheck()
    {
        if(isGrounded)
        {
            player.useGravity = false;
        }
        else
        {
            player.useGravity = true;
        }
    }

    void PlayerPause()
    {
        if (Input.GetKeyDown("p") || Input.GetKey(KeyCode.Escape))
        {
            pause = !pause;
        }

        if (pause)
        {
            T_pause.SetActive(true);
        }

        if (!pause)
        {
            T_pause.SetActive(false);
        }
    }

    public IEnumerator Apagao()
    {
        //objetos
        interactionTexts[valor].SetActive(true);
        yield return new WaitForSecondsRealtime(9);
        interactionTexts[valor].SetActive(false);
        Globals.Aparecer_Texto = false;
    }
    
    public IEnumerator Apagao2()
    {
        //visoes
        interactionTexts[contador].SetActive(true);
        yield return new WaitForSecondsRealtime(12);
        interactionTexts[contador].SetActive(false);
        Globals.Aparecer_Texto = false;
        olhando = false;
    }

    public IEnumerator Apagao3(bool _carta)
    {
        //colecionaveis
        if (!_carta)
        { 
            interactionTexts[valor2].SetActive(true);
            yield return new WaitForSecondsRealtime(18);
            interactionTexts[valor2].SetActive(false);
            Globals.Aparecer_Texto = false;
        }
        else
        {
            interactionTexts[valor].SetActive(true);
            yield return new WaitForSecondsRealtime(9);
            interactionTexts[valor].SetActive(false);
            Globals.Aparecer_Texto = false;
        }
    }

    public IEnumerator ApagaoFinal()
    {
        //colecionaveis
        interactionTexts[27].SetActive(true);
        yield return new WaitForSecondsRealtime(18);
        interactionTexts[27].SetActive(false);
        handScript.EndGameCredits();
        Globals.Aparecer_Texto = false;
    }

    public IEnumerator PortaTrancada()
    {
        //porta trancada
        interactionTexts[34].SetActive(true);
        yield return new WaitForSecondsRealtime(9);
        interactionTexts[34].SetActive(false);
        Globals.Aparecer_Texto = false;
    }

    public void SavePlayer()
    {
        PlayerPrefsX.SetIntArray("inventario", inventario);
        PlayerPrefsX.SetBoolArray("itens", acquiredColectables);
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            onLadder = true;
            player.useGravity = false;
            fpsController.enabled = false;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            onLadder = false;
            player.useGravity = true;
            fpsController.enabled = true;
        }
    }
}
