using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thug2AI : Enemy
{
    [SerializeField] private CutScene scene;
    private CircleCollider2D collide;
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collide = GetComponent<CircleCollider2D>();
    }
    //Checks to make sure that the cut scene has finished playing before the enemies go and attack
    protected override void Update()
    {
        base.Update();
        if (scene.cutSceneHasFinished) 
            Attack();
    }
    protected override void FixedUpdate()
    {
        if (scene.cutSceneHasFinished) 
            ChasePlayer();
    }
  
    protected override void ChasePlayer()
    {
       transform.position = Vector2.MoveTowards(transform.position, playerLocation.transform.position, moveSpeed * Time.deltaTime);
    }
    protected override void Attack()
    {
        if (playerInRange && !Dead())
        {
            if (Time.time > lastAttacked + attackSpeed)
            {
                animator.SetTrigger("isAttacking");
                playerHealth.takeDamage(damage);
                lastAttacked = Time.time;
            }
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }
    protected override void FlipSprite()
    {
        if(!Dead())
            spriteRend.flipX = playerLocation.transform.position.x > transform.position.x;
    }
    //Destroys collider of object after death but not sprite
    protected override void PlayDeathAnimation()
    {
        base.PlayDeathAnimation();
        if (Dead())
            Destroy(collide);
    }
}
