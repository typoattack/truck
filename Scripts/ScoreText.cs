using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreText : MonoBehaviour {

    public Text scoreText;

    private void Start()
    {
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
                         "Cumulative Score: " + PlayerController.totalScore + "\r\n" + 
                         "Total Coins: " + PlayerController.coins + "\r\n";
    }
}