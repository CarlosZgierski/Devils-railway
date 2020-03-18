using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameCredits : MonoBehaviour {

    public void EndCutscene()
    {
        SceneManager.LoadScene(0);
    }
}
