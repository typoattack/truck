using System.Collections;
using System.Collections.Generic;
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
        scoreText.text = "You were isekai'd!\r\n" +
                         "You scored " + PlayerController.score + " points\r\n" +
                         "You collected " + PlayerController.coins + " coins\r\n" +
                         "\r\n\r\nPress space to restart";
    }
}