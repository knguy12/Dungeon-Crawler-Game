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
        movement.movementSpeed += 3;
        yield return new WaitForSeconds(5f);
        movement.movementSpeed -= 3;
        Destroy(this.gameObject);
    }
    //When player uses their orb
    private void UsePower() 
    {
        if (Input.GetKeyDown("q") && playerInv.inventory.Contains("GreenOrb" + orbID))
        {
            //Plays useranimation and gives buff
            playerAnimator.Play("GreenPowerUp");
            PlayPowerUpSound();
            StartCoroutine(GreenPower());
            //Clears out UI and removes the orb from the player inventory
            orbProfile.color = Color.clear;
            playerInv.RemoveOrb("GreenOrb" + orbID);
            orbDescription.text = "";
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
