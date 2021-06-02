using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAI : Enemy
{
    private bool playerInCollision;
    private Vector2 playerLoc;
    private bool deadChecker = true;
    private float chargeTime = 1.0f;
    [SerializeField] private AudioSource Death;
    [SerializeField] private AudioSource Boom;

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
            if (Vector2.Distance(transform.position, playerLoc) < 1f)
            {
                int randomInt = UnityEngine.Random.Range(-3, 3);
                playerLoc = new Vector2(playerLocation.transform.position.x + randomInt, playerLocation.transform.position.y + randomInt);
            }
            //Checks if charger has reached players previous found location
            transform.position = Vector2.MoveTowards(transform.position, playerLoc, moveSpeed * Time.deltaTime);

            //If player collides with 2D collider have them take damage
            if (playerInCollision) 
            {
                Attack();
            }
        }
    }

    private void Charge() 
    {
        if (playerInRange && !animator.GetBool("isAttacking"))
        {
            Boom.Play();
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
    protected override void PlayDeathAnimation()
    {
        if (Dead() && deadChecker)
        {
            deadChecker = false;
            Death.Play();
            moveSpeed = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetTrigger("fatalDamageDone");
            Destroy(gameObject, 5f);
        }

    }
    //Removes unnecessary method from super class
    protected override void OnTriggerExit2D(Collider2D collision)
    {
    }
}
