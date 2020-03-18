using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

    public GameObject diario;
    public GameObject pause;

    public void Pause()
    {
        pause.SetActive(true);
        diario.SetActive(false);
    }

    public void Diario()
    {
        pause.SetActive(false);
        diario.SetActive(true);
    }

    public void Retornar()
    {
        Player.pause = !Player.pause;
    }

	public void RageQuit()
    {
        Application.Quit();
    }
}
