using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;


// This script is used to control the player objects
// A unique challenge here is that each player has control over 2 sprites, so we need to check against both rigidbodies 
// when checking for collisions!

public class Player : MonoBehaviour
{
    public AudioSource collisionBeep; // referenced by Ball script for beeping
    
    // Stores the inputs for each side
    public string nameAxis;
    public string _jumpAxis;
    public float jumpAxis;

    public Sprite neutral;
    public Sprite jumpjump;
    private SpriteRenderer activeSprite;

    // Used for movement
    public Rigidbody2D body;
    
    // Used to check against the other rigidbody to make sure there is no unauthorized movement
    public float minY;
    private float _move;
    
    public Boolean active;
    private bool jumping;
    
    // Start is called before the first frame update
    void Start()
    {
        active = false;
        body = GetComponent<Rigidbody2D>();
        activeSprite = GetComponent<SpriteRenderer>();
        jumping = false;
    }

    public void Activate()
    {
        active = true;
    }
    
    public void Deactivate()
    {
        active = false;
    }

    private void Update()
    {
        if (active)
        {
            Move(Input.GetAxis(nameAxis));
            if (Input.GetButtonDown(_jumpAxis)) Jump();
        }
        FixRotation();

        if (transform.position.y >= minY)
        {
            activeSprite.sprite = jumpjump;
        }
        else
        {
            activeSprite.sprite = neutral;
        }
    }
    
    private void FixedUpdate () 
    {
        if (active)
        {
            jumpAxis = body.velocity.y;

            if (jumping)
            {
                jumpAxis = 7.0f;
                jumping = false;
            }
		
            body.velocity = new Vector2(_move * 9.0f, jumpAxis);
        }
    }

    void FixRotation()
    {
        transform.rotation = new Quaternion(0,0,0,0);
    }
    
    public void Move(float input)
    {
        _move = Mathf.Clamp(input,-1,1);
    }
	
    public void Jump()
    {
        if (transform.position.y <= minY) jumping = true;
    }
	
    public bool Walking()
    {
        return _move != 0f;
    }
}
