using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thug1AI : Enemy
{
    [SerializeField] private int bulletSpeed;
    [SerializeField] private GameObject bulletPreFab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private CutScene scene;
    [SerializeField] private AudioManager aud;

    protected override void Start()
    {
        aud = GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();
        if (scene.cutSceneHasFinished)
            Attack();
    }
    //Removes Patrol as Thug will always be in a state of attack
    protected override void FixedUpdate()
    {
        CheckIfMoving();
        if (scene.cutSceneHasFinished) 
            ChasePlayer();
    }
    //Moves the thug in up and down based on the y value of the player
    protected override void ChasePlayer()
    {
        Vector2 playerYPosition = new Vector2(transform.position.x, playerLocation.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, playerYPosition, moveSpeed * Time.deltaTime);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            animator.SetBool("playerDetected", true);
            moveSpeed = 0;
        }
    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            animator.SetBool("playerDetected", false);
            moveSpeed = 3;
        }
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPreFab, bulletSpawn.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = playerLocation.transform.position - transform.position;
        rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
    }
    protected override void Attack()
    {
        if (animator.GetBool("playerDetected") && !Dead() && !animator.GetBool("isMoving"))
        {
            if (Time.time > lastAttacked + attackSpeed)
            {
                animator.SetTrigger("isAttacking");
                aud.Play("Attack");
                Shoot();
                lastAttacked = Time.time;
            }
        }
    }
    protected override void FlipSprite()
    {
        if(!Dead())
            spriteRend.flipX = playerLocation.transform.position.x > transform.position.x;
    }
    //Switches between idle and walking animation
    private void CheckIfMoving() 
    {
        if (rb.IsSleeping())
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);
    }
}
