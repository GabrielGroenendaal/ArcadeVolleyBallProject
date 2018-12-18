using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// This script will be used to control the overal logic of the game

public class GameController : MonoBehaviour
{
    
    // For State Control
    private string gameState;
    
    // For beeps
    public AudioSource beep;
    
    // For the Game
    public Volleyball game;
    
    // For controlling text
    public Text mainText;
    public Text Player1Controls;
    public Text Player2Controls;
    public Text Player1Score;
    public Text Player2Score;
    
    // For final screen
    private string winnerName;
    private string loserName;
    private int winnerScore;
    private int loserScore;
        
    // Start is called before the first frame update
    void Start()
    {
        gameState = "menu";
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "menu")
        {
            game.playGame = false;
            mainText.enabled = true;
            Player1Controls.enabled = true;
            Player2Controls.enabled = true;
            
            mainText.text = "Ready to Play? Press SPACE to Start!";
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                beep.Play();
                gameState = "game";
            }

        }

        else if (gameState == "game")
        {
            game.playGame = true;
            mainText.enabled = false;
            Player1Controls.enabled = false;
            Player2Controls.enabled = false;
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                beep.Play();
                Reset();
                gameState = "menu";
            }
            
            else if (game.p1score == 15)
            {
                loserScore = game.p2score;
                winnerScore = game.p1score;
                winnerName = "Player 1";
                loserName = "Player 2";
                gameState = "GameOver";
            }
            
            else if (game.p2score == 15)
            {
                loserScore = game.p1score;
                winnerScore = game.p2score;
                winnerName = "Player 2";
                loserName = "Player 1";
                gameState = "GameOver";
            }
        }

        else if (gameState == "GameOver")
        {
            game.playGame = false;
            mainText.enabled = true;
            mainText.text = winnerName + " beat " + loserName + " by " + (winnerScore - loserScore) + " points";
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                beep.Play();
                Reset();
                gameState = "menu";
            }
        }
        
    }

    void Reset()
    {
        winnerName = "";
        winnerScore = 0;
        loserScore = 0;
        Player1Score.text = "0";
        Player2Score.text = "0";
    }
}
