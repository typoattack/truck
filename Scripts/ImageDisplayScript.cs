using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDisplayScript : MonoBehaviour {

    CharacterMainMenu menu;
    private int ability;
    private int currentAbility = 0;
    private int maxNumberOfAbilities = 7;

    // Use this for initialization
    void Start ()
    {
        menu = GameObject.Find("CharacterMainMenu").GetComponent<CharacterMainMenu>();
        ability = menu.ability;
        currentAbility = ability;
        displayImage(currentAbility);
    }
	
	// Update is called once per frame
	void Update ()
    {
        ability = menu.ability;

        if (currentAbility != ability)
        {
            currentAbility = ability;
            displayImage(currentAbility);
        }
    }

    void displayImage(int ability)
    {
        for (int a = 0; a < maxNumberOfAbilities; a++)
        {
            if (a == ability) gameObject.transform.GetChild(a).gameObject.SetActive(true);
            else gameObject.transform.GetChild(a).gameObject.SetActive(false);
        }
    }
}
