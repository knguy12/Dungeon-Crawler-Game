using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAI : Enemy
{
    private bool playerInCollision;
    private float chargeTime = 1.0f;
    protected override void Update()
    {
        base.Update();
        Charge();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.name == "Player")
            playerInCollision = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        playerInCollision = false;
    }
    protected override void ChasePlayer()
    {
        //Charger chases after player location and if the charger reaches player location with collision then it will try to find the location of the player again
        if (animator.GetBool("isAttacking")) 
        {
            transform.position = Vector2.MoveTowards(transform.position, targetLocation, moveSpeed * Time.deltaTime);
            //Checks if charger has reached players previous found location
            if (Vector2.Distance(transform.position, targetLocation) < 1f)
                targetLocation = playerLocation.transform.position;
            //If player collides with 2D collider have them take damage
            if (playerInCollision)
                Attack();
        }
    }
    private void Charge() 
    {
        if (playerInRange && !animator.GetBool("isAttacking"))
        {
            chargeTime -= Time.deltaTime;
            //If charge time is less then zero then find player location and increase the speed of charger
            if (chargeTime < 0)
            {
                animator.SetBool("isAttacking", true);
                targetLocation = playerLocation.transform.position;
                moveSpeed = 4;
            }
        }
    }
    protected override void FlipSprite()
    {
        spriteRend.flipX = targetLocation.x > transform.position.x;
    }
    //Plays enemy proc animation when player enters collider and stops charger from moving for a short duration
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !animator.GetBool("isAttacking"))
        {
            moveSpeed = 0;
            animator.SetTrigger("playerDetected");
            playerInRange = true;
        }
    }
    //Removes unnecessary method from super class
    protected override void OnTriggerExit2D(Collider2D collision)
    {
    }
}
