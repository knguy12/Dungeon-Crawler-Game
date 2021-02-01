using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    private List<Vector2> waypoints;
    protected Rigidbody2D rb;

    protected Vector2 targetLocation;
    protected float lastAttacked;
    protected bool playerInRange;
    protected bool hitWall;
    
    [SerializeField] protected GameObject playerLocation;
    [SerializeField] protected int moveSpeed;
    [SerializeField] protected Health playerHealth;
    [SerializeField] protected int damage;
    [SerializeField] protected int attackSpeed;

    public SpriteRenderer spriteRend;
    public string enemyName;
    public bool enemyIsDead;
    public EnemyHealth enemyHealth;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        targetLocation = transform.position;
        waypoints = new List<Vector2>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FlipSprite();
        PlayDeathAnimation();
    }
    protected virtual void FixedUpdate()
    {
        Patrol();
        ChasePlayer();
    }
    protected virtual void Patrol()
    {
        //If the enemy has not detected an enemy then it will patrol towards a random location
        if (!animator.GetBool("isAttacking")) 
        {
            transform.position = Vector2.MoveTowards(transform.position, targetLocation, moveSpeed * Time.deltaTime);
            //Checks if the enemy has reached its patrol location and assigns it a new one
            if (Vector2.Distance(transform.position, targetLocation) < 1f)
                GetPatrolPosition();
        }
    }
    protected virtual void ChasePlayer() 
    {
        //If player enters trigger then enemy will start chasing player
        if (animator.GetBool("isAttacking"))
            transform.position = Vector2.MoveTowards(transform.position, playerLocation.transform.position, moveSpeed * Time.deltaTime);
    }
    //If player enters the trigger then playerDetected will be set to true
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isAttacking", true);
            playerInRange = true;
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }
    //Gets random location for bomber to move to with a max distance of 2 from its previous position in the x and y direction
    protected virtual void GetPatrolPosition()
    {
        targetLocation = new Vector2(transform.position.x + UnityEngine.Random.Range(-5, 5), transform.position.y + UnityEngine.Random.Range(-5, 5));
        UnstuckEnemy(targetLocation);
    }
    //If player is in range of enemy attack then have them take damage per second based on attack speed
    protected virtual void Attack() 
    {
        if (Time.time > lastAttacked + attackSpeed)
        {
            playerHealth.takeDamage(damage);
            lastAttacked = Time.time;
        }
    }
    //Playes death animation when enemy dies
    protected virtual void PlayDeathAnimation() 
    {
        if (Dead()) 
        {
            moveSpeed = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animator.SetTrigger("fatalDamageDone");
        }
    }
    //Checks if enemy runs into a wall and turns it around
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "TileMap_Base" || collision.gameObject.name == "TileMap_Door") 
        {
            targetLocation = -targetLocation;
            UnstuckEnemy(targetLocation);
        }
    }
    //Checks if enemy is stuck bouncing between two waypoints and tries to find a new waypoint out of it 
    private void UnstuckEnemy(Vector2 target) 
    {
        if (waypoints == null)
            return;
        if(waypoints.Contains(target))
            GetPatrolPosition();
        else
            waypoints.Add(target);
    }
    //Flips sprite on the horizontal based on its intended location
    protected virtual void FlipSprite() 
    {
        if (!animator.GetBool("isAttacking")) 
            spriteRend.flipX = targetLocation.x > transform.position.x;
        else
            spriteRend.flipX = playerLocation.transform.position.x > transform.position.x;
    }
    //If enemy is stuck on wall find new patrol position
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        GetPatrolPosition();
    }
    public void TakeDamage(int damage) 
    {
        enemyHealth.takeDamage(damage);
    }
    //Checks if enemy is dead
    public bool Dead() 
    {
        if (enemyHealth.GetCurrentHealth() <= 0)
            return true;
        else
            return false;
    }
}
