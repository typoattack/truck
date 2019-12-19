using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinMenu : MonoBehaviour
{

    CharacterMainMenu menu;
    public int[] skinLocks = new int[8];
    public int[] skinUnlockThreshold = { 0, 10, 20, 30, 40, 50, 60, 70 };
    private int skin;
    public int currentskin;
    private int coins;
    private int totalAbilities = 8;

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
        if (!PlayerPrefs.HasKey("TotalCoins"))
        {
            PlayerPrefs.SetInt("TotalCoins", 0);
        }
        else
        {
            coins = PlayerPrefs.GetInt("TotalCoins");
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
        if (skinLocks[skin] == 2)
        {
            gameObject.transform.GetChild(5).gameObject.SetActive(false);
            gameObject.transform.GetChild(6).gameObject.SetActive(false);
            gameObject.transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (skinLocks[skin] == 1)
        {
            gameObject.transform.GetChild(5).gameObject.SetActive(false);
            gameObject.transform.GetChild(6).gameObject.SetActive(true);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
        }
        else if (skinLocks[skin] == 0 && coins >= skinUnlockThreshold[skin])
        {
            gameObject.transform.GetChild(5).gameObject.SetActive(false);
            skinLocks[skin] = 1;
            PlayerPrefsX.SetIntArray("SkinLocks", skinLocks);
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(6).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(4).gameObject.SetActive(false);
            gameObject.transform.GetChild(5).gameObject.SetActive(true);
            gameObject.transform.GetChild(6).gameObject.SetActive(false);
        }
    }

    public void purchase()
    {
        coins -= skinUnlockThreshold[currentskin];
        PlayerPrefs.SetInt("TotalCoins", coins);
        skinLocks[currentskin] = 2;
        PlayerPrefsX.SetIntArray("SkinLocks", skinLocks);
        displayButton(currentskin);
    }
}
