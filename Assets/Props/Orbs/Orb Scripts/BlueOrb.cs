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
        if (Input.GetKeyDown("q") && inventory.Contains("BlueOrb")) 
        {
            playerHealth.heal(20);
            orbProfile.color = Color.clear;
            orbDescription.text = "";
            inventory.Remove("BlueOrb");
            Destroy(gameObject, 1f);
        }
    }
}
