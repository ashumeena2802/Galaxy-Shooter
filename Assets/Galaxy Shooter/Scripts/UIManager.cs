using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // sprite for lives 
    public Sprite[] lives;
    public Image livesImageDisplay;

    public GameObject titleScreen;

    public Text scoreText;
    public int score;

    public void UpdateLives(int currentLives)
    {
        // sprite image display by the current Lives
        livesImageDisplay.sprite = lives[currentLives];
        
    }

    public void UpdateScore()
    {
        // Adding the score + 10
        score += 10;
        //  updating the score
        scoreText.text = "Score : " + score;


    }

    // for showing the title screen
    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);

    }

    // for hiding the title screen
    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);

        // reset the score
        score = 0;
        scoreText.text = "Score : ";

    }
    
    
}
