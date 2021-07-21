using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreText : MonoBehaviour {

    public Text scoreText;
    private int coins;
    private int topScore;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("TotalCoins"))
        {
            PlayerPrefs.SetInt("TotalCoins", 0);
        }
        else
        {
            coins = PlayerPrefs.GetInt("TotalCoins");
        }
        if (!PlayerPrefs.HasKey("TopScore"))
        {
            PlayerPrefs.SetInt("TopScore", 0);
        }
        else
        {
            topScore = PlayerPrefs.GetInt("TopScore");
        }

        SetScoreText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
    }

    private void SetScoreText()
    {
        scoreText.text = "Round Score: " + PlayerController.score + "\r\n" +
                         "High Score: " + topScore + "\r\n" + 
                         "Coins: " + coins + "\r\n";
    }
}