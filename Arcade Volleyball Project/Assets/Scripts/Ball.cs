using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a pretty simple script and is basically just for keeping track of the ball and its speed
public class Ball : MonoBehaviour
{
    public AudioSource beep;
    public Rigidbody2D ballbody;
    private float ballSpeed = 8.0f;

    // Stores how many repeated bounces the ball has one side
    public int sameSideBounces;
    // Stores who the last person who bounced it is. Every time this changes, sameSideBounces changes
    public string lastBounce;


    private Vector2 LastVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        ballbody = GetComponent<Rigidbody2D>();
        ballbody.velocity = new Vector2(0,0);
        ballbody.angularVelocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isStill()
    {
        return ballbody.velocity.magnitude == 0;
    }

    private void FixedUpdate()
    {
        LastVelocity = ballbody.velocity;
       
        if (isStill()) return;
        ballSpeed += .001f;
        ballbody.velocity = ballbody.velocity.normalized *  ballSpeed;
        ballbody.AddForce(new Vector2(0,-.01f));
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 surfaceNormal = other.contacts[0].normal;
        ballbody.velocity = Vector2.Reflect(LastVelocity, surfaceNormal);
        
        // If the ball hits the floor or the pole
        if (other.gameObject.CompareTag("Floor"))
        {
            if (transform.position.x < 0) // If it's Team A, the win goes to Team B
            {
                GameOver(false);
            }
            else // And vice versa
            {
                GameOver(true);
            }
        }
        else if (other.gameObject.CompareTag("Walls")||other.gameObject.CompareTag("Pole"))
        {
            beep.Play();
        }
        
        else if (other.gameObject.CompareTag("TeamA"))
        {
            if (lastBounce == "TeamA")
            {
                sameSideBounces++;
                if (sameSideBounces > 3) // Checks number of bounces
                {
                    GameOver(false); // If 3 bounces, gives win to TeamA (Left)
                }
                other.gameObject.GetComponent<AudioSource>().Play();
            }
            else
            {
                sameSideBounces = 1;
                lastBounce = "TeamA";
            }

        }
        
        // If a player from teamB hits the ball
        else if (other.gameObject.CompareTag("TeamB"))
        {
            if (lastBounce == "TeamB") 
            {
                sameSideBounces++; 
                if (sameSideBounces > 3)  // Checks number of bounces
                {
                    GameOver(true); // If 3 bounces, gives win to TeamA (Left)
                }
                other.gameObject.GetComponent<AudioSource>().Play();
            }
            else
            {
                sameSideBounces = 1;
                lastBounce = "TeamB";
            }
        }
    }
    void GameOver(Boolean leftSideWon)
    {
        GameObject.Find("Game Controller").GetComponent<Volleyball>().RoundMatch(leftSideWon);
    }
}
