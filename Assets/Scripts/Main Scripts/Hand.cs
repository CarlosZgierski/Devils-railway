using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Hand : MonoBehaviour
{
    private GameObject item;

    public GameObject BotaoE;

    public float rayCastRange = 3;
    RaycastHit hit;

    private Player playerRef;

    [SerializeField] private Image blackScreen;
    [SerializeField] private Animator fadeAnimation;

    private void Start()
    {
        playerRef = GetComponentInParent<Player>();
    }

    private void Update()
    {
        Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector3.forward) * rayCastRange);

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit,rayCastRange))
        {
            if (hit.collider.CompareTag(Globals.OBJECTS_TAG) || hit.collider.CompareTag(Globals.INTERACTION_TAG) || hit.collider.CompareTag(Globals.CIPO_TAG) || hit.collider.CompareTag(Globals.DOOR_TAG) || hit.collider.CompareTag(Globals.STAIR_ZONE_TAG) || hit.collider.CompareTag(Globals.TELEGRAPIC_TAG))
            {
                if (!Globals.Aparecer_Texto)
                {
                    BotaoE.SetActive(true);
                }
                item = hit.collider.gameObject;
            }

            if (hit.collider.CompareTag(Globals.UNTAGGED_TAG))
            {
                BotaoE.SetActive(false);
                item = hit.collider.gameObject;
            }

            if (hit.collider.CompareTag(Globals.LEVEL01_LEVEL02))
            {
                if (!Globals.Aparecer_Texto)
                {
                    BotaoE.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerRef.SavePlayer();
                    StartCoroutine(ChangeScene2());
                }
            }

            if (hit.collider.CompareTag(Globals.LEVEL02_LEVEL03) )
            {
                if(!Globals.Aparecer_Texto)
                {
                    BotaoE.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerRef.SavePlayer();
                    StartCoroutine(ChangeScene3());
                }
            }

            
            if (Input.GetKeyDown(KeyCode.E))
            {
                BotaoE.SetActive(false);
               
            }
        }
        else
        {
            item = null;
            BotaoE.SetActive(false);
        }
    }


    public GameObject SelectedObject()
    {
        if (item != null)
        {
            return item;
        }
        else return null;
    }

    public void EndGameCredits()
    {
        StartCoroutine(ChangeFinalScene());
    }

    IEnumerator ChangeScene2()
    {
        fadeAnimation.SetBool("Fade", true);
        yield return new WaitUntil(()=> blackScreen.color.a == 1);
        SceneManager.LoadScene(1);
    }

    IEnumerator ChangeScene3()
    {
        fadeAnimation.SetBool("Fade", true);
        yield return new WaitUntil(() => blackScreen.color.a == 1);
        SceneManager.LoadScene(2);
    }

    IEnumerator ChangeFinalScene()
    {
        fadeAnimation.SetBool("Fade", true);
        yield return new WaitUntil(() => blackScreen.color.a == 1);
        SceneManager.LoadScene(3);
    }
}
