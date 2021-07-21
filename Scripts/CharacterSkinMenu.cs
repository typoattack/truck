using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSkinMenu : MonoBehaviour
{
    CharacterMainMenu menu;
    public int[] skinLocks = new int[2];
    public int[] skinUnlockThreshold = { 0, 20 };
    private int skin;
    public int currentskin;
    private int topScore;
    private int totalSkins = 2;
    public int isSkinValid = 0;

    public Text skinUnlockText;

    void Start()
    {
        menu = GameObject.Find("CharacterMainMenu").GetComponent<CharacterMainMenu>();
        skinLocks = menu.skinLocks;
        if (!PlayerPrefs.HasKey("SkinLocks"))
        {
            PlayerPrefsX.SetIntArray("SkinLocks", skinLocks);
        }
        else
        {
            skinLocks = PlayerPrefsX.GetIntArray("SkinLocks");
        }
        skin = menu.skin;
        currentskin = skin;
        
        if (!PlayerPrefs.HasKey("TopScore"))
        {
            PlayerPrefs.SetInt("TopScore", 0);
        }
        else
        {
            topScore = PlayerPrefs.GetInt("TopScore");
        }

        for (int i = 0; i < totalSkins; i++)
        {
            if (skinLocks[i] == 0 && topScore >= skinUnlockThreshold[i])
            {
                skinLocks[i] = 1;
            }
        }

        displayButton(currentskin);
    }

    void Update()
    {
        skin = menu.skin;

        if (currentskin != skin)
        {
            currentskin = skin;
            displayButton(currentskin);
        }
    }

    

    void displayButton(int skin)
    {
        skinUnlockText.text = "High score of " + skinUnlockThreshold[skin] + " needed to unlock";

        if (skinLocks[skin] == 1)
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            //gameObject.transform.GetChild(3).gameObject.SetActive(true);
            isSkinValid = 1;
        }
        /*
        else if (skinLocks[skin] == 0 && topScore >= skinUnlockThreshold[skin])
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            skinLocks[skin] = 1;
            PlayerPrefsX.SetIntArray("skinLocks", skinLocks);
            //gameObject.transform.GetChild(3).gameObject.SetActive(true);
            isSkinValid = 0;
        }
        */
        else
        {
            //gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            isSkinValid = 0;
        }
    }
}
