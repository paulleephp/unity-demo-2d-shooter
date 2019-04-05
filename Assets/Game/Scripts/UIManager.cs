using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public int score = 0;
    public GameObject titleScreen;

    private bool hasGameStarted = false;

    public void UpdateLives(int currentLives)
    {
        Debug.Log(currentLives);
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;

        scoreText.text = "Score: " + score;
    }

    public void ShowTitle()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitle()
    {
        titleScreen.SetActive(false);

        // reset the score
        score = 0;

        scoreText.text = "Score: ";
    }

}
