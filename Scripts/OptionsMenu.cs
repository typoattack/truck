using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

    public void GoToCustomization()
    {
        SceneManager.LoadScene("Character Select", LoadSceneMode.Single);
    }

    public void ResetAllValues()
    {
        PlayerPrefs.SetInt("TotalCoins", 0);
        PlayerPrefs.SetInt("TotalScore", 0);
    }

    public void MaxAllValues()
    {
        PlayerPrefs.SetInt("TotalCoins", 99999);
        PlayerPrefs.SetInt("TotalScore", 99999);
    }
}
