using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour {

    CharacterMainMenu menu;
    private int skin;
    private int currentskin = 0;
    private int maxNumberOfSkins = 2;

	// Use this for initialization
	void Start ()
    {
        menu = GameObject.Find("CharacterMainMenu").GetComponent<CharacterMainMenu>();
        skin = menu.skin;
        currentskin = skin;
        displayModel(currentskin);
    }
	
	// Update is called once per frame
	void Update ()
    {
        skin = menu.skin;

        if (currentskin != skin)
        {
            currentskin = skin;
            displayModel(currentskin);
        }
    }

    void displayModel(int skin)
    {
        for (int s = 0; s < maxNumberOfSkins; s++)
        {
            if (s == skin)
            {
                gameObject.transform.GetChild(s).gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.GetChild(s).gameObject.SetActive(false);
            }
        }
    }
}
