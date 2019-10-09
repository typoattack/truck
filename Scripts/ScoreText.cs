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
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    private void SetScoreText()
    {
        scoreText.text = "Score: " + PlayerController.score + "\r\n" +
                         "Coins: " + PlayerController.coins + "\r\n";
    }
}