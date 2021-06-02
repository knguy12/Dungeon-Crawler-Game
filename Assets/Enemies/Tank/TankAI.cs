using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : Enemy
{
    [SerializeField] private int bulletSpeed;
    [SerializeField] private int bulletAttackSpeed;
    [SerializeField] private GameObject bulletPreFab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private int numberOfProjectiles;
    [SerializeField] private AudioSource Sphere;
    [SerializeField] private AudioSource Hit;
    [SerializeField] private AudioSource Death;

    private float radius;

    protected override void Start()
    {
        base.Start();
        radius = 10f;
    }
    protected override void Update()
    {
        base.Update();
        bulletAttack();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            animator.SetBool("playerDetected", true);
            playerInRange = true;
            moveSpeed = 0;
        }
    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.CompareTag("Player"))
        {
            moveSpeed = 2;
            animator.SetBool("playerDetected", false);
        }
    }
    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
        if (collision.gameObject.name == "Player")
        {
            animator.SetBool("isAttacking", true);
            Attack();
        }
    }
    protected override void Attack()
    {
        if (Time.time > lastAttacked + attackSpeed)
        {
            Hit.Play();
            playerHealth.takeDamage(damage);
            lastAttacked = Time.time;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
            animator.SetBool("isAttacking", false);
    }
    private void bulletAttack()
    {
        if (playerInRange && !animator.GetBool("isAttacking"))
        {
            if (Time.time > lastAttacked + attackSpeed)
            {
                Shoot();
                Sphere.Play();
                lastAttacked = Time.time;
            }
        }
    }
    private void Shoot()
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            //Finds the x and y direction based on the number of projectiles
            float projectileDirXposition = bulletSpawn.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = bulletSpawn.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = projectileVector - (Vector2)bulletSpawn.transform.position;

            //Instantiates the bullet and fires it
            GameObject bullet = Instantiate(bulletPreFab, bulletSpawn.transform.position, Quaternion.identity);
            //Forces bullets to ignore the collider of the enemy firing it
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(projectileMoveDirection * bulletSpeed, ForceMode2D.Impulse);

            angle += angleStep;
        }
    }
    protected override void PlayDeathAnimation()
    {
        if (Dead())
        {
            moveSpeed = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Death.Play();
            animator.SetTrigger("fatalDamageDone");
            Destroy(gameObject, 5f);
        }
    }
}
