using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAI : Enemy
{
    [SerializeField] private int bulletSpeed;
    [SerializeField] private GameObject bulletPreFab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private AudioSource Fire;
    [SerializeField] private AudioSource Death;
    private bool deadOneShot = true;


    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Attack();
        PlayDeathAnimation();
    }
    protected override void Attack()
    {
        if (playerInRange)
        {
            if (Time.time > lastAttacked + attackSpeed)
            {
                animator.SetTrigger("isAttacking");
                Fire.Play();
                Shoot();
                lastAttacked = Time.time;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            playerInRange = true;
    }
    //Intializes the bullet gameobject and finds the direction of the player and fires it accoridng to that
    private void Shoot() 
    {
        GameObject bullet = Instantiate(bulletPreFab, bulletSpawn.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = playerLocation.transform.position - transform.position;
        rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
    }
    protected override void PlayDeathAnimation()
    {
        if (Dead() && deadOneShot)
        {
            moveSpeed = 0;
            Death.Play();
            animator.SetTrigger("fatalDamageDone");
            Destroy(gameObject, 5f);
            deadOneShot = false;
        }
    }
    //Yes I know this is bad but I dont care so cry more
    protected override void ChasePlayer()
    {
    }
    protected override void FlipSprite()
    {
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
    }
    protected override void Patrol()
    {
    } 
}
