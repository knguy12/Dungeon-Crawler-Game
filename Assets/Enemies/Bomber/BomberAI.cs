using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BomberAI : Enemy
{
    [SerializeField] private float detonationTime;
    [SerializeField] private CameraShake camShake;
    [SerializeField] private GameObject enemyHealthDisplay;
    private bool oneShotSound = true;
    private bool oneShotExplode = true;
    private bool bombHasBlown = false;

    [SerializeField] private AudioSource Fuse;
    [SerializeField] private AudioSource Explode;


    protected override void ChasePlayer()
    {
        if (animator.GetBool("isAttacking")) 
        {
            transform.position = Vector2.MoveTowards(transform.position, playerLocation.transform.position, moveSpeed * Time.deltaTime);
            //Playes fuse sound effect exactly once
            if (oneShotSound) 
            {
                Fuse.Play();
                oneShotSound = false;
            }
            //Counts down before bomber begins detonating bomb
            detonationTime -= Time.deltaTime;
            if (detonationTime < 0)
            {
                moveSpeed = 0;
                Fuse.Stop();
                animator.SetTrigger("fatalDamageDone");
                //Playes the bomb explosion sound effect exactly once
                if (oneShotExplode) 
                {
                    Explode.Play();
                    oneShotExplode = false;
                }
                Attack();
                enemyHealthDisplay.SetActive(false);
            }
        }
    }
    protected override void Attack()
    {
        //Checks if player is range and that the bomb has not already blown before doing damage
        if (playerInRange && !bombHasBlown)
        {
            //Triggers camera shake and deals damage to player
            camShake.TriggerShake(0.2f, 0.07f);
            playerHealth.takeDamage(damage);
            bombHasBlown = true;
        }
        Destroy(gameObject, 5f);
    }
    //Plays death animation and does damage to player if enemy gets destroyed
    protected override void PlayDeathAnimation()
    {
        if (enemyHealth.GetCurrentHealth() <= 0) 
        {
            moveSpeed = 0;
            animator.SetTrigger("fatalDamageDone");
            //Playes explosion sound effect if player kills enemy
            if (oneShotExplode)
            {
                Explode.Play();
                oneShotExplode = false;
            }
            Attack();
        }
    }

}
