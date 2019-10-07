using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
