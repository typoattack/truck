using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

    private int[] abilitiesLocked = { 2, 0, 0, 0, 0, 0, 0 };
    private int[] abilitiesUnlocked = { 2, 2, 2, 2, 2, 2, 2 };
    private int[] skinsLocked = { 1, 0 };
    private int[] skinsUnlocked = { 1, 1 };

    public void GoToCustomization()
    {
        SceneManager.LoadScene("Character Select", LoadSceneMode.Single);
    }

    public void ResetAllValues()
    {
        PlayerPrefs.SetInt("TotalCoins", 0);
        PlayerPrefs.SetInt("TotalScore", 0);
        PlayerPrefsX.SetIntArray("AbilityLocks", abilitiesLocked);
        PlayerPrefsX.SetIntArray("SkinLocks", skinsLocked);
        PlayerPrefs.SetInt("Ability", 0);
        PlayerPrefs.SetInt("Gender", 0);
        PlayerPrefs.SetInt("Skin", 0);
    }

    public void MaxAllValues()
    {
        PlayerPrefs.SetInt("TotalCoins", 99999);
        PlayerPrefs.SetInt("TotalScore", 99999);
        PlayerPrefsX.SetIntArray("AbilityLocks", abilitiesUnlocked);
        PlayerPrefsX.SetIntArray("SkinLocks", skinsUnlocked);
    }
}
