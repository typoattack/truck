using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMainMenu : MonoBehaviour {

    public int ability = 0, gender = 0, skin = 0;
    private int totalAbilities = 8, totalSkins = 8;

    public int[] abilityLocks = { 1, 0, 0, 0, 0, 0, 0, 0 };
    public int[] skinLocks = { 2, 0, 0, 0, 0, 0, 0, 0 };

    void Start()
    {
        if (!PlayerPrefs.HasKey("Ability"))
        {
            PlayerPrefs.SetInt("Ability", 0);
        }
        else
        {
            ability = PlayerPrefs.GetInt("Ability");
        }

        if (!PlayerPrefs.HasKey("Gender"))
        {
            PlayerPrefs.SetInt("Gender", 0);
        }
        else
        {
            gender = PlayerPrefs.GetInt("Gender");
        }

        if (!PlayerPrefs.HasKey("Skin"))
        {
            PlayerPrefs.SetInt("Skin", 0);
        }
        else
        {
            skin = PlayerPrefs.GetInt("Skin");
        }

        if (!PlayerPrefs.HasKey("AbilityLocks"))
        {
            PlayerPrefsX.SetIntArray("AbilityLocks", abilityLocks);
        }
        else
        {
            abilityLocks = PlayerPrefsX.GetIntArray("AbilityLocks");
        }
        if (!PlayerPrefs.HasKey("SkinLocks"))
        {
            PlayerPrefsX.SetIntArray("SkinLocks", abilityLocks);
        }
        else
        {
            skinLocks = PlayerPrefsX.GetIntArray("SkinLocks");
        }
    }

    void Update()
    {
        ability = ability % totalAbilities;
        skin = skin % totalSkins;
        gender = gender % 2;
    }

    public void AddToAbilities()
    {
        ability++;
        if (ability == totalAbilities) ability = 0;
    }

    public void SubtractFromAbilities()
    {
        ability--;
        if (ability < 0) ability = totalAbilities - 1;
    }

    public void ConfirmAbility()
    {
        PlayerPrefs.SetInt("Ability", ability);
    }

    public void AddToSkin()
    {
        skin++;
        if (skin == totalSkins) skin = 0;
    }

    public void SubtractFromSkin()
    {
        skin--;
        if (skin < 0) skin = totalSkins - 1;
    }

    public void ConfirmSkin()
    {
        PlayerPrefs.SetInt("Gender", gender);
        PlayerPrefs.SetInt("Skin", skin);
    }

    public void AddToGender()
    {
        gender++;
    }

    public void ExitToMenu()
    {
        //PlayerPrefs.SetInt("Ability", ability);
        //PlayerPrefs.SetInt("Gender", gender);
        //PlayerPrefs.SetInt("Skin", skin);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
