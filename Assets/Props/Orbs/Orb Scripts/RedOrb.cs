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
        attack.playerAttackDamage += 20;
        movement.movementSpeed -= 1;
        yield return new WaitForSeconds(10f);
        attack.playerAttackDamage -= 20;
        movement.movementSpeed += 1;
    }
    private void UsePower() 
    {
        if (Input.GetKeyDown("q") && inventory.Contains("RedOrb")) 
        {
            playerAnimator.Play("RedPowerUp");
            StartCoroutine(RedPower());
            orbProfile.color = Color.clear;
            orbDescription.text = "";
            inventory.Remove("RedOrb");
            Destroy(gameObject, 11f);
        }
    }
}
