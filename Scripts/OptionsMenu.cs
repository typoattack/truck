using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

    private int[] abilityLocks = { 0, 0, 0, 0, 0, 0, 0, 0 };

    public void GoToCustomization()
    {
        SceneManager.LoadScene("Character Select", LoadSceneMode.Single);
    }

    public void ResetAllValues()
    {
        PlayerPrefs.SetInt("TotalCoins", 0);
        PlayerPrefs.SetInt("TotalScore", 0);
        PlayerPrefsX.SetIntArray("AbilityLocks", abilityLocks);
    }

    public void MaxAllValues()
    {
        PlayerPrefs.SetInt("TotalCoins", 99999);
        PlayerPrefs.SetInt("TotalScore", 99999);
    }
}
