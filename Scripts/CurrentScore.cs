using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurrentScore : MonoBehaviour {

    public Text scoreText;

    private void Start()
    {
        SetScoreText();
    }

    private void FixedUpdate()
    {
        SetScoreText();
    }

    private void SetScoreText()
    {
        scoreText.text = "Score: " + PlayerController.score;
    }
}
