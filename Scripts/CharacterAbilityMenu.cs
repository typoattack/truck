using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilityMenu : MonoBehaviour {

    CharacterMainMenu menu;
    private int[] abilityLocks = new int[8];
    private int[] abilityUnlockThreshold = { 0, 10, 20, 30, 40, 50, 60, 70 };
    private int ability;
    private int currentAbility;
    private int totalScore;
    private int totalAbilities = 8;
    
    void Start ()
    {
        menu = GameObject.Find("CharacterMainMenu").GetComponent<CharacterMainMenu>();
        abilityLocks = menu.abilityLocks;
        if (!PlayerPrefs.HasKey("AbilityLocks"))
        {
            PlayerPrefsX.SetIntArray("AbilityLocks", abilityLocks);
        }
        else
        {
            abilityLocks = PlayerPrefsX.GetIntArray("AbilityLocks");
        }
        ability = menu.ability;
        currentAbility = ability;
        if (!PlayerPrefs.HasKey("TotalScore"))
        {
            PlayerPrefs.SetInt("TotalScore", 0);
        }
        else
        {
            totalScore = PlayerPrefs.GetInt("TotalScore");
        }
        displayButton(currentAbility);
    }
	
	void Update ()
    {
        ability = menu.ability;

        if (currentAbility != ability)
        {
            currentAbility = ability;
            displayButton(currentAbility);
        }
    }

    void displayButton(int ability)
    {
        if (abilityLocks[ability] == 1)
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (abilityLocks[ability] == 0 && totalScore >= abilityUnlockThreshold[ability])
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            abilityLocks[ability] = 1;
            PlayerPrefsX.SetIntArray("AbilityLocks", abilityLocks);
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
        }
    }
}
