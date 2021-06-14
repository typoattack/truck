using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySkinCheck : MonoBehaviour {

    CharacterMainMenu menu;

    void Start()
    {
        menu = GameObject.Find("CharacterMainMenu").GetComponent<CharacterMainMenu>();

    }

    void Update()
    {
        if (menu.skin == PlayerPrefs.GetInt("Skin"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
