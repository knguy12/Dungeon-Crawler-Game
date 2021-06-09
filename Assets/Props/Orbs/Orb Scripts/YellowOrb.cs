using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowOrb : Orb
{
    private PlayerAttack attack;
    protected override void Start()
    {
        base.Start();
        attack = player.GetComponent<PlayerAttack>();
    }
    protected override void Update()
    {
        base.Update();
        UsePower();
    }
    //Increases player attakc range and speed for 10 seocnds 
    private IEnumerator YellowPower() 
    {
        attack.attackRange += 1;
        attack.attackSpeed += 4;
        yield return new WaitForSeconds(5f);
        attack.attackRange -= 1;
        attack.attackSpeed -= 4;
        Destroy(this.gameObject);
    }
    private void UsePower() 
    {
        if (Input.GetKeyDown("q") && playerInv.inventory.Contains("YellowOrb" + orbID)) 
        {
            playerAnimator.Play("YellowPowerUp");
            PlayPowerUpSound();
            StartCoroutine(YellowPower());
            //Removes orb values from UI
            orbProfile.color = Color.clear;
            orbDescription.text = "";
            playerInv.RemoveOrb("YellowOrb" + orbID);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
