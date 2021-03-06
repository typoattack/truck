﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMainMenu : MonoBehaviour {

    public int ability = 0, skin = 0;
    private int totalAbilities = 7, totalSkins = 2;

    public int[] abilityLocks = { 2, 0, 0, 0, 0, 0, 0, 0 };
    public int[] skinLocks = { 1, 0 };

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
        PlayerPrefs.SetInt("Skin", skin);
    }
    

    public void ExitToMenu()
    {
        //PlayerPrefs.SetInt("Ability", ability);
        //PlayerPrefs.SetInt("Skin", skin);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
