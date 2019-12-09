using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour {

    CharacterMainMenu menu;
    private int ability, gender;
    private int currentAbility, currentGender = 0;

	// Use this for initialization
	void Start ()
    {
        menu = GameObject.Find("CharacterMainMenu").GetComponent<CharacterMainMenu>();
        ability = menu.ability;
        gender = menu.gender;
        currentAbility = ability;
        currentGender = gender;
        displayModel(currentAbility, currentGender);
    }
	
	// Update is called once per frame
	void Update ()
    {
        ability = menu.ability;
        gender = menu.gender;

        if (currentAbility != ability || currentGender != gender)
        {
            currentAbility = ability;
            currentGender = gender;
            displayModel(currentAbility, currentGender);
        }
    }

    void displayModel(int ability, int gender)
    {
        for (int g = 0; g <= 1; g++)
        {
            if (g == gender)
            {
                for (int a = 0; a <= 7; a++)
                {
                    if (a == ability)
                    {
                        gameObject.transform.GetChild(0).GetChild(g).GetChild(a).gameObject.SetActive(true);
                        gameObject.transform.GetChild(1).GetChild(g).GetChild(a).gameObject.SetActive(true);
                    }
                    else
                    {
                        gameObject.transform.GetChild(0).GetChild(g).GetChild(a).gameObject.SetActive(false);
                        gameObject.transform.GetChild(1).GetChild(g).GetChild(a).gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                for (int a = 0; a <= 7; a++)
                {
                    gameObject.transform.GetChild(0).GetChild(g).GetChild(a).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).GetChild(g).GetChild(a).gameObject.SetActive(false);
                }
            }
        }
    }
}
