using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinMenu : MonoBehaviour
{

    CharacterMainMenu menu;
    public int[] skinLocks = new int[2];
    public int[] skinUnlockThreshold = { 0, 100 };
    private int skin;
    public int currentskin;
    private int totalScore;
    private int totalSkins = 2;
    public int isSkinValid = 0;

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
        
        if (!PlayerPrefs.HasKey("TotalScore"))
        {
            PlayerPrefs.SetInt("TotalScore", 0);
        }
        else
        {
            totalScore = PlayerPrefs.GetInt("TotalScore");
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
        if (skinLocks[skin] == 1)
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            //gameObject.transform.GetChild(3).gameObject.SetActive(true);
            isSkinValid = 1;
        }
        else if (skinLocks[skin] == 0 && totalScore >= skinUnlockThreshold[skin])
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            skinLocks[skin] = 1;
            PlayerPrefsX.SetIntArray("skinLocks", skinLocks);
            //gameObject.transform.GetChild(3).gameObject.SetActive(true);
            isSkinValid = 0;
        }
        else
        {
            //gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
            isSkinValid = 0;
        }
    }
}
