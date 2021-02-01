using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GreenOrb : Orb
{
    private Movement movement;
    protected override void Start()
    {
        base.Start();
        movement = player.GetComponent<Movement>();
    }
    protected override void Update()
    {
        base.Update();
        UsePower();
    }
    //Gives player 5 extra movment speed for 10 seconds
    private IEnumerator GreenPower() 
    {
        movement.movementSpeed += 5;
        yield return new WaitForSeconds(10f);
        movement.movementSpeed -= 5;
    }
    //When player uses their orb
    private void UsePower() 
    {
        if (Input.GetKeyDown("q") && inventory.Contains("GreenOrb"))
        {
            //Plays useranimation and gives buff
            playerAnimator.Play("GreenPowerUp");
            StartCoroutine(GreenPower());
            //Clears out UI and removes the orb from the player inventory
            orbProfile.color = Color.clear;
            inventory.Remove("GreenOrb");
            orbDescription.text = "";
            //Destroys object after buff is over
            Destroy(gameObject, 11f);
        }
    }
}
