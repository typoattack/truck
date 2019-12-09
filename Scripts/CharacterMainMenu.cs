using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMainMenu : MonoBehaviour {

    public int ability = 0, gender = 0;
    private int totalAbilities = 8;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Ability"))
        {
            PlayerPrefs.SetInt("Ability", 0);
        }
        else
        {
            ability = PlayerPrefs.GetInt("Ability");
        }

        if (!PlayerPrefs.HasKey("Gender"))
        {
            PlayerPrefs.SetInt("Gender", 0);
        }
        else
        {
            gender = PlayerPrefs.GetInt("Gender");
        }
    }

    void Update()
    {
        ability = ability % totalAbilities;
        gender = gender % 2;
    }

    void AddToAbilities()
    {
        ability++;
    }

    void SubtractFromAbilities()
    {
        ability--;
    }

    void AddToGender()
    {
        gender++;
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
