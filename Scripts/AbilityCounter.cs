using UnityEngine;
using UnityEngine.UI;

public class AbilityCounter : MonoBehaviour
{

    public Text scoreText;

    private void Start()
    {
        SetScoreText();
    }

    private void Update()
    {
        SetScoreText();
    }

    private void SetScoreText()
    {
        scoreText.text = "" + PlayerController.counter;
    }
}