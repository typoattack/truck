using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAbilityMenu : MonoBehaviour {

    CharacterMainMenu menu;
    private int[] abilityLocks = new int[7];
    private int[] abilityUnlockThreshold = { 0, 10, 20, 30, 40, 50, 70 };
    private int ability;
    private int currentAbility;
    private int coins;
    private int totalAbilities = 7;
    public int isAbilityValid = 0;

    public Text abilityUnlockText;
    
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
        if (!PlayerPrefs.HasKey("TotalCoins"))
        {
            PlayerPrefs.SetInt("TotalCoins", 0);
        }
        else
        {
            coins = PlayerPrefs.GetInt("TotalCoins");
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
        abilityUnlockText.text = abilityUnlockThreshold[ability] + " coins needed to unlock";

        if (abilityLocks[ability] == 2)
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);
            //gameObject.transform.GetChild(3).gameObject.SetActive(true);
            isAbilityValid = 1;
        }
        else if (coins >= abilityUnlockThreshold[ability])
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            abilityLocks[ability] = 1;
            PlayerPrefsX.SetIntArray("abilityLocks", abilityLocks);
            //gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(true);
            isAbilityValid = 0;
        }
        else
        {
            abilityLocks[ability] = 0;
            PlayerPrefsX.SetIntArray("abilityLocks", abilityLocks);
            //gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            gameObject.transform.GetChild(5).gameObject.SetActive(false);
            isAbilityValid = 0;
        }
    }

    public void purchase()
    {
        coins -= abilityUnlockThreshold[currentAbility];
        PlayerPrefs.SetInt("TotalCoins", coins);
        abilityLocks[currentAbility] = 2;
        PlayerPrefsX.SetIntArray("abilityLocks", abilityLocks);
        displayButton(currentAbility);
    }
}
