using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject Inicial;
    public GameObject creditos;
    public GameObject controls;
    public CameraMov cam;

    void Start()
    {
       
    }

        #region public Functions

    public void PlayButton()
    {
        Destroy(Inicial.gameObject);
        cam.GetComponent<CameraMov>().PlayAnim();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameCredits()
    {
        Inicial.SetActive(false);
        creditos.SetActive(true);
    }

    public void Controls()
    {
        Inicial.SetActive(false);
        controls.SetActive(true);
    }

    public void N_GameCredits()
    {
        creditos.SetActive(false);
    }

    public void N_Controls()
    {
        controls.SetActive(false);
    }

    #endregion
}
