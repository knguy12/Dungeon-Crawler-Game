using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOrb : Orb
{
    private PlayerAttack attack;
    private Movement movement;
    protected override void Start()
    {
        base.Start();
        attack = player.GetComponent<PlayerAttack>();
        movement = player.GetComponent<Movement>();
    }
    protected override void Update()
    {
        base.Update();
        UsePower();
    }
    //Increases players attack damage and movement speed for 10 seconds
    IEnumerator RedPower() 
    {
        attack.playerAttackDamage += 10;
        yield return new WaitForSeconds(5f);
        attack.playerAttackDamage -= 10;
        Destroy(this.gameObject);
    }
    private void UsePower() 
    {
        if (Input.GetKeyDown("q") && playerInv.inventory.Contains("RedOrb" + orbID))
        {
            playerAnimator.Play("RedPowerUp");
            PlayPowerUpSound();
            StartCoroutine(RedPower());
            orbProfile.color = Color.clear;
            orbDescription.text = "";
            playerInv.RemoveOrb("RedOrb" + orbID);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
