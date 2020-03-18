
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTest : MonoBehaviour
{

    Rigidbody player;

    public CursorLockMode wantedMode;

    public float vel = 20f;
    public float sensitivity = 2f;
    public float jumpHeight = 0.2f;

    float moveFB;
    float moveLR;
    float moveLadder;
    bool onLadder;

    float lookZ;
    float lookX;

    public Camera eyes;

    bool canZoom;

    public GameObject vision;
    public GameObject hand;
    HandTest handScript;
    VisionTest visionScript;

    GameObject item;
    int keyAtual;
    int ultimoItem;
    int ultimaKey;

    Vector3[] teste = new Vector3[5];
    private int[] inventario = new int [6];
    InventoryTest inventarioPossivel;

    public FeetTest feet;
    bool isGrounded;

    float actualFoV;
    private bool zoom = true;

    public float fovInicial;
    public float zoomMaximo;


    // Use this for initialization
    void Start()
    {
        Cursor.lockState = wantedMode;

        player = GetComponent<Rigidbody>();
        handScript = hand.GetComponent<HandTest>();
        visionScript = vision.GetComponent<VisionTest>();
        inventarioPossivel = GetComponent<InventoryTest>();

        if (fovInicial == 0)
        {
            fovInicial = 70f;
        }
        if (zoomMaximo == 0)
        {
            zoomMaximo = fovInicial - 20f;
        }

        actualFoV = fovInicial;

        eyes.fieldOfView = actualFoV;

        if (feet == null)
        {
            feet = GetComponentInChildren<FeetTest>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = feet.TouchedGround();
        eyes.fieldOfView = actualFoV;

        PlayerMovement();
        PlayerInteraction();
        PlayerInventory();
        PlayerJump();
        FieldOfView();
        PlayerVelocityCap();

        print(player.velocity);

        //inventario na hud()
        for(int x = 0 ; x < inventario.Length ; x ++ )
        {
            int iD = inventario[x];
            Sprite item;

            if (iD == 0)
            {
                break;
            }
            else
            {
                item = inventarioPossivel.possiveisItems[iD];
                inventarioPossivel.posiHuds[x].GetComponent<Image>().sprite = item;
            }

        }

        /*
        for(int x = 0; x< inventario.Length ;x++)
        {
            if (inventario[x] != null) 
            print(inventario[x] + ", Slot:" + x);
        }
        */
    }

    void PlayerMovement()
    {
        moveFB = Input.GetAxis("Vertical") * vel;
        moveLR = Input.GetAxis("Horizontal") * vel * -1;
        moveLadder = Input.GetAxis("Vertical") * (vel/10);

        lookX = Input.GetAxis("Mouse Y") *-1;
        lookZ = Input.GetAxis("Mouse X");

        
        Vector3 movement = new Vector3(moveFB, 0 ,moveLR);
        movement = this.transform.rotation * movement;

        this.transform.Rotate(0, lookZ * sensitivity, 0);
        eyes.transform.Rotate(lookX * sensitivity, 0, 0);

        if (!onLadder)
        {
            player.velocity += (movement * Time.deltaTime);
        }
        else
        {
            Vector3 latterM = new Vector3(0, moveLadder, 0);
            this.transform.position += latterM * Time.deltaTime;
        }

		if (Input.GetKeyDown (KeyCode.LeftShift)) 
		{
			vel = vel * 2;
		}
		else if (Input.GetKeyUp (KeyCode.LeftShift)) 
		{
			vel = vel / 2;
		}
        
    }

    void PlayerVelocityCap()
    {
        if (player.velocity.magnitude > 10)
        {
            player.velocity = player.velocity.normalized * 10;
        }
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !onLadder)
        {
            player.AddForce(0, jumpHeight*10000, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && onLadder)
        {
            player.velocity += new Vector3(-jumpHeight, 0, 0);
        }
    }

    void PlayerInteraction()
    {
        if(Input.GetKeyDown("e"))
        {
            item = handScript.SelectedObject();
            if (item.CompareTag(GlobalsTest.OBJECTS_TAG))
            {
                if (item != null)
                {
                    for (int x = 0; x < inventario.Length; x++)
                    {
                        if (inventario[x] == 0)
                        {
                            inventario[x] =item.GetComponent<ObjectTest>().objectNumber;
                            item.GetComponent<ObjectTest>().SelfDestruct();
                            break;
                        }
                    }
                }
                else
                {
                    print("nada");
                }
            }
            else if(item.CompareTag(GlobalsTest.INTERACTION_TAG))
            {
                if(item.GetComponent<InteractionZoneTest>().ItemNecessario(ultimoItem))
                {
                    inventario[ultimaKey] = 0;
                    ultimoItem = 0;
                    item.SetActive(false);
                }
            }
            else if(item.CompareTag(GlobalsTest.CIPO_TAG))
            {
                item.GetComponent<CipoTest>().DestroyCipo();
            }
            else if(item.CompareTag(GlobalsTest.DOOR_TAG))
            {
                item.GetComponent<DoorTest>().DoorInteraction();
            }
        }
        canZoom = visionScript.SeeVisionObject();
        if(!canZoom)
        {
            zoom = true;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if(canZoom)
            {
                zoom = !zoom;
            }
        }
    }

    void PlayerInventory()
    {
        
        if(int.TryParse(Input.inputString, out keyAtual))
        {
            if (keyAtual < inventario.Length)
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

	void FieldOfView ()
	{
        
        if (!zoom)
        {
            if (eyes.fieldOfView > zoomMaximo)
                actualFoV = Mathf.Lerp(actualFoV, zoomMaximo, 10 * Time.deltaTime);
        }
        else
        {
            if (eyes.fieldOfView < fovInicial)
                actualFoV = Mathf.Lerp(actualFoV, fovInicial, 10 * Time.deltaTime);
        }
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
