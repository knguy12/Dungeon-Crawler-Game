using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D body;

    Animator player_Animator;

    public float horizontal;
    public float vertical;
    public float LastMoveX;
    public float LastMoveY;

    
    public float xLeftClamp;
    public float xRightClamp;
    public float yLeftClamp;
    public float yRightClamp;
    

    public UnityEngine.Vector2 spawnPoint;

    public float movementSpeed = 7;
    public bool canMove = true;

    public float xDirectionFacedWhenSpawn;
    public float yDirectionFacedWhenSpawn;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player_Animator = GetComponent<Animator>();
        //Sets the spawn point of player
        transform.position = spawnPoint;
        //Sets Player in proper direction when changing scences
        player_Animator.SetFloat("LastMoveX", xDirectionFacedWhenSpawn);
        player_Animator.SetFloat("LastMoveY", yDirectionFacedWhenSpawn);
    }

    void Update()
    {
        // allows idle animation to play as it means player is not in motion
        UnityEngine.Vector3 movement = new UnityEngine.Vector3(horizontal, vertical, 0.0f);
        player_Animator.SetFloat("Magnitude", movement.magnitude);

        if (canMove == true)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            //Changes idle animation depending on whether last input was to the left, right, up, or down
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                LastMoveX = Input.GetAxisRaw("Horizontal"); 
                LastMoveY = Input.GetAxisRaw("Vertical");
                player_Animator.SetFloat("LastMoveX", horizontal);
                player_Animator.SetFloat("LastMoveY", vertical);
            }
            //Set values for Animator to decide which animation to play
            player_Animator.SetFloat("Horizontal", movement.x);
            player_Animator.SetFloat("Vertical", movement.y);
        }
        //Clamps map in order to ensure player does not go outside it
        //transform.position = new UnityEngine.Vector3(Mathf.Clamp(body.position.x, xLeftClamp, xRightClamp), Mathf.Clamp(body.position.y, yLeftClamp, yRightClamp), transform.position.z);
    }

    private void FixedUpdate()
    {
        //Checks if player is moving diagonally and reduces speed
        if (horizontal != 0 && vertical != 0)
        {
            horizontal = horizontal * 0.7f;
            vertical = vertical * 0.7f;
        }
        //Checks if player can move and moves player according to movement speed
        if (canMove == true)
        {
            body.velocity = new UnityEngine.Vector2(horizontal * movementSpeed, vertical * movementSpeed);
        }
        //If player cannot move then stop player immediately and turn off both horizontal and vertical animation
        else
        {
            horizontal = 0;
            vertical = 0;
            body.velocity = new UnityEngine.Vector2(0, 0);
        }
    }
}
