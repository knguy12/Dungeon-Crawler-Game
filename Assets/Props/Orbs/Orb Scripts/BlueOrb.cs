using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOrb : Orb
{
    private Health playerHealth;
    protected override void Start()
    {
        base.Start();
        playerHealth = player.GetComponent<Health>();
    }
    protected override void Update()
    {
        base.Update();
        UsePower();
    }
    //Heals player for 20 health on use
    private void UsePower() 
    {
        if (Input.GetKeyDown("q") && playerInv.inventory.Contains("BlueOrb" + orbID)) 
        {
            playerAnimator.Play("BluePowerUp");
            PlayPowerUpSound();
            playerHealth.heal(50);
            orbProfile.color = Color.clear;
            orbDescription.text = "";
            playerInv.RemoveOrb("BlueOrb" + orbID);
            Destroy(this.gameObject);
        }
    }
}
