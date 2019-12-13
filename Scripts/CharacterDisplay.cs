using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour {

    CharacterMainMenu menu;
    private int skin, gender;
    private int currentskin, currentGender = 0;
    private int maxNumberOfSkins = 8;

	// Use this for initialization
	void Start ()
    {
        menu = GameObject.Find("CharacterMainMenu").GetComponent<CharacterMainMenu>();
        skin = menu.skin;
        gender = menu.gender;
        currentskin = skin;
        currentGender = gender;
        displayModel(currentskin, currentGender);
    }
	
	// Update is called once per frame
	void Update ()
    {
        skin = menu.skin;
        gender = menu.gender;

        if (currentskin != skin || currentGender != gender)
        {
            currentskin = skin;
            currentGender = gender;
            displayModel(currentskin, currentGender);
        }
    }

    void displayModel(int skin, int gender)
    {
        for (int g = 0; g <= 1; g++)
        {
            if (g == gender)
            {
                for (int s = 0; s < maxNumberOfSkins; s++)
                {
                    if (s == skin)
                    {
                        gameObject.transform.GetChild(0).GetChild(g).GetChild(s).gameObject.SetActive(true);
                        gameObject.transform.GetChild(1).GetChild(g).GetChild(s).gameObject.SetActive(true);
                    }
                    else
                    {
                        gameObject.transform.GetChild(0).GetChild(g).GetChild(s).gameObject.SetActive(false);
                        gameObject.transform.GetChild(1).GetChild(g).GetChild(s).gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                for (int s = 0; s < maxNumberOfSkins; s++)
                {
                    gameObject.transform.GetChild(0).GetChild(g).GetChild(s).gameObject.SetActive(false);
                    gameObject.transform.GetChild(1).GetChild(g).GetChild(s).gameObject.SetActive(false);
                }
            }
        }
    }
}
