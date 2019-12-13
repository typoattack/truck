using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

    public void GoToCustomization()
    {
        SceneManager.LoadScene("Character Select", LoadSceneMode.Single);
    }
}
