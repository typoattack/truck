using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour {

    public void Replay()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
