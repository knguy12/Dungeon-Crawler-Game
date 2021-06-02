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
        yield return new WaitForSeconds(10f);
        attack.attackRange -= 1;
        attack.attackSpeed -= 4;
    }
    private void UsePower() 
    {
        if (Input.GetKeyDown("q") && inventory.Contains("YellowOrb")) 
        {
            playerAnimator.Play("YellowPowerUp");
            PlayPowerUpSound();
            StartCoroutine(YellowPower());
            //Removes orb values from UI
            orbProfile.color = Color.clear;
            orbDescription.text = "";
            inventory.Remove("YellowOrb");
            Destroy(gameObject, 11f);
        }
    }
}
