using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BomberAI : Enemy
{
    [SerializeField] private float detonationTime;
    [SerializeField] private CameraShake camShake;
    [SerializeField] private GameObject enemyHealthDisplay;
    private bool bombHasBlown;

    protected override void ChasePlayer()
    {
        if (animator.GetBool("isAttacking")) 
        {
            transform.position = Vector2.MoveTowards(transform.position, playerLocation.transform.position, moveSpeed * Time.deltaTime);
            //Counts down before bomber begins detonating bomb
            detonationTime -= Time.deltaTime;
            if (detonationTime < 0)
            {
                moveSpeed = 0;
                animator.SetTrigger("fatalDamageDone");
                Attack();
                enemyHealthDisplay.SetActive(false);
            }
        }
    }
    protected override void Attack()
    {
        if (playerInRange && !bombHasBlown)
        {
            camShake.TriggerShake(0.2f, 0.07f);
            playerHealth.takeDamage(damage);
            bombHasBlown = true;
        }
    }
    //Plays death animation and does damage to player if they are in range
    protected override void PlayDeathAnimation()
    {
        if (enemyHealth.GetCurrentHealth() <= 0) 
        {
            moveSpeed = 0;
            animator.SetTrigger("fatalDamageDone");
            Attack();
        }
    }

}
