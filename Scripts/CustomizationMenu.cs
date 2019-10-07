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
    }

    public void setAbility()
    {
        PlayerPrefs.SetInt("Ability", GetComponentInChildren<Dropdown>().value);
    }
}
