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
        movement.movementSpeed += 5;
        attack.playerAttackDamage += 20;
        attack.attackRange += 1;
        attack.attackSpeed += 4;
        yield return new WaitForSeconds(10f);
        movement.movementSpeed -= 5;
        attack.playerAttackDamage -= 20;
        attack.attackRange -= 1;
        attack.attackSpeed -= 4;
    }
    private void UsePower() 
    {
        if (Input.GetKeyDown("q") && inventory.Contains("WhiteOrb"))
        {
            playerAnimator.Play("WhitePowerUp");
            StartCoroutine(WhitePower());
            orbProfile.color = Color.clear;
            inventory.Remove("WhiteOrb");
            orbDescription.text = "";
            Destroy(gameObject, 11f);
        }
    }
}
