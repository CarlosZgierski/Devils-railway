
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class PlayerBackup : MonoBehaviour
{
    Rigidbody player;

    //Modo do cursor na tela
    public CursorLockMode wantedMode;

    //variaveis de velocidade da camera e velocidade do jogador
    public float vel = 100f;
    public float sensitivity = 2f;
    public float jumpHeight = 0.2f;

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

    //Variaveis para que o jogador faça somente um pulo
    public Feet feet;
    bool isGrounded;

    //Variaveis para o zoom da camera e do Field of View do jogador
    float actualFoV;
    private bool zoom = true;
    public float fovInicial;
    public float zoomMaximo;

    //variaveis para a escrita da visao
    int contador;
    bool show; 
    public GameObject[] interactionTexts;

    //Variaveis para o zoom em certos objetos
    bool canZoom;
    public GameObject vision;
    Vision visionScript;

    //arrays para o armazenamento das bool de item colecionaveis e documentos pegos pelo jogador
    private bool[] acquiredDocuments = new bool [5];
    private bool[] acquiredColectables = new bool [5];

    //ajudar na coroutine
    public int valor;

    void Start()
    {
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

        //pegar as referencias necessarias nas children do player
        if (feet == null)
        {
            feet = GetComponentInChildren<Feet>();
        }
        if(playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }
        if(handScript == null)
        {
            handScript = GetComponentInChildren<Hand>();
        }
    }

    void Update()
    {
        isGrounded = feet.TouchedGround();
        playerCamera.fieldOfView = actualFoV;

        GravityCheck();
        PlayerMovement();
        PlayerInteraction();
        PlayerInventory();
        PlayerInventoryHud();
        PlayerJump();
        FovChanger();
        PlayerVelocityCap();
        Playerthought();

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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !onLadder)
        {
             player.AddForce(0, jumpHeight * 10000, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && onLadder)
        {
            player.velocity += new Vector3(-jumpHeight, 0, 0);
        }
    }

    void PlayerVelocityCap()
    {
        if (player.velocity.magnitude > 10)
        {
            player.velocity = player.velocity.normalized * 10;
        }
    }

    void Playerthought()
    {
        contador = visionScript.ZoomObjectId();

        if(show)
        {
          interactionTexts[contador].SetActive(true);
        }
        StartCoroutine("Pensamento2");
    }

    void PlayerInteraction()
    {
        if(Input.GetKeyDown("e"))
        {
            item = handScript.SelectedObject();
            if (item.CompareTag(Globals.OBJECTS_TAG))
            {
                if (!item.GetComponent<InteractiveObject>().colecionavel || !item.GetComponent<InteractiveObject>().documento)
                {
                    for (int x = 0; x < inventario.Length; x++)
                    {
                        if (inventario[x] == 0)
                        {
                            inventario[x] = item.GetComponent<InteractiveObject>().objectNumber;
                            item.GetComponent<InteractiveObject>().SelfDestruct(inventarioPossivel);
                            break;
                        }
                    }
                }
                else if (item.GetComponent<InteractiveObject>().colecionavel)
                {
                    //coloca uma bool true para os objectos que o jogador ja pegoue  deleta o objeto
                    acquiredColectables [ item.GetComponent<InteractiveObject>().objectNumber % 20] = true;

                    item.GetComponent<InteractiveObject>().SelfDestruct();
                }
                else
                {
                    acquiredDocuments[item.GetComponent<InteractiveObject>().objectNumber % 10] = true;

                    item.GetComponent<InteractiveObject>().SelfDestruct();
                }
            }
            else if (item.CompareTag(Globals.INTERACTION_TAG))
            {
                if(item.GetComponent<InteractionZone>().ItemNecessario(ultimoItem))
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
                    if(item.GetComponent<Door>().LockedDoorInteraction(ultimoItem))
                    {
                        inventario[ultimaKey] = 0;
                        ultimoItem = 0;
                    }
                }
            }
            else if (item.CompareTag(Globals.STAIR_ZONE_TAG))
            {
                if(!item.GetComponent<StairZone>().ActivateLadder(ultimoItem))
                {
                    inventario[ultimaKey] = 0;
                    ultimoItem = 0;
                }
                else
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
            }
        }

        canZoom = visionScript.SeeVisionObject();
        if (!canZoom)
        {
            show = false;
            zoom = true;
        }
        

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (canZoom)
            {
                show = !show;
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
                break;
            }
            else
            {
                valor = iD;
                item = inventarioPossivel.possiveisItems[iD];
                inventarioPossivel.posiHuds[x].GetComponent<Image>().sprite = item;
                interactionTexts[iD].SetActive(true);
                StartCoroutine("Pensamento");
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

    private IEnumerator Pensamento()
    {
        yield return new WaitForSecondsRealtime(10);
        interactionTexts[valor].SetActive(false);
    }

    private IEnumerator Pensamento2()
    {
        yield return new WaitForSecondsRealtime(10);
        interactionTexts[contador].SetActive(false);
    }


    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            onLadder = true;
            player.useGravity = false;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            onLadder = false;
            player.useGravity = true;
        }
    }
}
