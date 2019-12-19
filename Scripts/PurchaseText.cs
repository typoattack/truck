using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PurchaseText : MonoBehaviour
{
    CharacterSkinMenu menu;
    public Text purchaseText;
    private int[] prices = new int[8];
    private int currentSkin;

    private void Start()
    {
        //menu = GameObject.Find("CharacterSkinMenu").GetComponent<CharacterSkinMenu>();
        //prices = menu.skinUnlockThreshold;
        //currentSkin = menu.currentskin;
    }

    private void OnEnable()
    {
        menu = GameObject.Find("CharacterSkinMenu").GetComponent<CharacterSkinMenu>();
        prices = menu.skinUnlockThreshold;
        currentSkin = menu.currentskin;
        SetPurchaseText();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene("Main", LoadSceneMode.Single);

        //currentSkin = menu.currentskin;
    }

    private void SetPurchaseText()
    {
        purchaseText.text = "Price: " + prices[currentSkin] + "\r\n" +
                         "Coins: " + PlayerPrefs.GetInt("TotalCoins") + "\r\n";
    }
}