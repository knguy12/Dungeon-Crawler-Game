using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteOrb : Orb
{
    private Movement movement;
    private PlayerAttack attack;

    protected override void Start()
    {
        base.Start();
        movement = player.GetComponent<Movement>();
        attack = player.GetComponent<PlayerAttack>();
    }
    protected override void Update()
    {
        base.Update();
        UsePower();
    }
    //Increases all player stats for 10 seconds
    private IEnumerator WhitePower() 
    {
        movement.movementSpeed += 3;
        attack.playerAttackDamage += 20;
        attack.attackRange += 1;
        attack.attackSpeed += 4;
        yield return new WaitForSeconds(5f);
        movement.movementSpeed -= 3;
        attack.playerAttackDamage -= 20;
        attack.attackRange -= 1;
        attack.attackSpeed -= 4;
        Destroy(this.gameObject);
    }
    private void UsePower() 
    {
        if (Input.GetKeyDown("q") && playerInv.inventory.Contains("WhiteOrb" + orbID))
        {
            playerAnimator.Play("WhitePowerUp");
            PlayPowerUpSound();
            StartCoroutine(WhitePower());
            orbProfile.color = Color.clear;
            playerInv.RemoveOrb("WhiteOrb" + orbID);
            orbDescription.text = "";
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
