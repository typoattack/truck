using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayExitButton : MonoBehaviour {

    CharacterSkinMenu skin;
    private int isSkinValid = 0;
    private int currentSkinValid = 0;
    CharacterAbilityMenu ability;
    private int isAbilityValid = 0;
    private int currentAbilityValid = 0;

    public Button exitButton;

    // Use this for initialization
    void Start ()
    {
		skin = GameObject.Find("CharacterSkinMenu").GetComponent<CharacterSkinMenu>();
        isSkinValid = skin.isSkinValid;
        currentSkinValid = isSkinValid;
        ability = GameObject.Find("CharacterAbilityMenu").GetComponent<CharacterAbilityMenu>();
        isAbilityValid = ability.isAbilityValid;
        currentAbilityValid = isAbilityValid;
        DisplayButton(currentSkinValid, currentAbilityValid);
    }
	
	// Update is called once per frame
	void Update ()
    {
        isSkinValid = skin.isSkinValid;
        isAbilityValid = ability.isAbilityValid;

        if (currentAbilityValid != isAbilityValid || currentSkinValid != isSkinValid)
        {
            currentSkinValid = isSkinValid;
            currentAbilityValid = isAbilityValid;
            DisplayButton(currentSkinValid, currentAbilityValid);
        }
    }

    void DisplayButton (int skin, int ability)
    {
        if (skin == 1 && ability == 1)
        {
            //gameObject.transform.GetChild(1).gameObject.SetActive(true);
            //gameObject.transform.GetChild(0).gameObject.SetActive(false);
            exitButton.interactable = true;
        }
        else
        {
            //gameObject.transform.GetChild(1).gameObject.SetActive(false);
            //gameObject.transform.GetChild(0).gameObject.SetActive(true);
            exitButton.interactable = false;
        }
    }
}
