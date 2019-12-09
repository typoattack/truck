using UnityEngine;
using UnityEngine.UI;

public class CustomizationMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("Ability"))
        {
            PlayerPrefs.SetInt("Ability", 0);
        }
        else
        {
            GetComponentInChildren<Dropdown>().value = PlayerPrefs.GetInt("Ability");
        }

        if (!PlayerPrefs.HasKey("Gender"))
        {
            PlayerPrefs.SetInt("Gender", 0);
        }
        else
        {
            GetComponentInChildren<Dropdown>().value = PlayerPrefs.GetInt("Gender");
        }
    }

    public void setAbility()
    {
        PlayerPrefs.SetInt("Ability", GetComponentInChildren<Dropdown>().value);
    }

    public void setGender()
    {
        PlayerPrefs.SetInt("Gender", GetComponentInChildren<Dropdown>().value);
    }
}
