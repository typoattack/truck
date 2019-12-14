using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAbilityCheck : MonoBehaviour {

    CharacterMainMenu menu;
    
    void Start ()
    {
        menu = GameObject.Find("CharacterMainMenu").GetComponent<CharacterMainMenu>();
        
    }
	
	void Update ()
    {
        if (menu.ability == PlayerPrefs.GetInt("Ability"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
