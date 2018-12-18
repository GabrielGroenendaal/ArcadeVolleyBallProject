using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

// This is where the main bulk of the code will go

public class Volleyball : MonoBehaviour
{
    // For developing prefabs
    [SerializeField] private GameObject player1;
    [SerializeField] 
    private GameObject player2;
    [SerializeField] 
    private GameObject volleyballLeft;
    [SerializeField] 
    private GameObject volleyballRight;
    
    // for Storing Characters
    private GameObject player1temp;
    private GameObject player2temp;
    private GameObject balltemp;
    
    // Scores
    public int p1score;
    public int p2score;
    public Text player1score;
    public Text player2score;
    
    // GameController
    public Boolean playGame;
    public Boolean theRightServes = true; // stores which side to start the ball on
    public Boolean thereIsNoBall;
    
    // Timer
    public float timer = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        thereIsNoBall = true;
        playGame = false;
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (playGame == true)
        {
            while (timer >= 0)
            {
                timer -= Time.deltaTime;
                return;
            }
            ActivateGame(); // Activates all the players
            
            if (thereIsNoBall) {
                SpawnVolleyball(); // Spawns the Volleyball on the appropiate side
                thereIsNoBall = false;
            }
        }

        if (playGame != true)
        {
            DeactivateGame();
        }
        
        
    }
    
    // Used to create the Ball
    void SpawnVolleyball()
    {
        if (theRightServes)
        {
            balltemp = Instantiate(volleyballRight);
        }
        else
        {
            balltemp = Instantiate(volleyballLeft);
        }
    }
    
    // Activates All the Prefabs
    void ActivateGame()
    {
        player1temp.GetComponent<Player>().Activate();
        player2temp.GetComponent<Player>().Activate(); 
    }
    
    void DeactivateGame()
    {
        player1temp.GetComponent<Player>().Deactivate();
        player2temp.GetComponent<Player>().Deactivate(); 
    }
    
    // This code is called when someone wins a point
    public void RoundMatch(Boolean leftSideWon)
    {
        if (leftSideWon) // Left side won
        {
            theRightServes = true;
            p1score++;
        }
        else
        {
            theRightServes = false;
            p2score++;
        }
        UpdateScore();
        DestroyAll();
        thereIsNoBall = true;
        timer = 1.5f;
        GameObject.Find("Game Controller").GetComponent<GameController>().beep.Play();
    }
    
    // Updates the score text
    void UpdateScore()
    {
        player1score.text = p1score.ToString();
        player2score.text = p2score.ToString();
    }
    
    // Starts up the game with fresh prefabs
    void ResetGame()
    {
        player1temp = Instantiate(player1);
        player2temp = Instantiate(player2);
    }
    
    // Destroys all the Prefabs
    void DestroyAll()
    {
        Destroy(player1temp);
        Destroy(player2temp);
        Destroy(balltemp);
        ResetGame();
    }
    
}
