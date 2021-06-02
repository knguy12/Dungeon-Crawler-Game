using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : Enemy
{
    [SerializeField] private GameObject bulletPreFab;
    [SerializeField] private Transform bulletSpawn1;
    [SerializeField] private Transform bulletSpawn2;
    [SerializeField] private int bulletSpeed;
    private Transform bulletSpawn;
    [SerializeField] private CutScene scene;
    [SerializeField] private AudioSource attackSound;

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
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }
    IEnumerator DoubleShot()
    {
        bulletSpawn = bulletSpawn1;
        Shoot();
        yield return new WaitForSeconds(1);
        bulletSpawn = bulletSpawn2;
        Shoot();
    }
    protected override void Attack()
    {
        if (playerInRange && !Dead())
        {
            if (Time.time > lastAttacked + attackSpeed)
            {
                attackSound.Play();
                animator.SetTrigger("isAttacking");
                StartCoroutine(DoubleShot());
                lastAttacked = Time.time;
            }
        }
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPreFab, bulletSpawn.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = playerLocation.transform.position - transform.position;
        rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
    }
    protected override void FlipSprite()
    {
        spriteRend.flipX = playerLocation.transform.position.x > transform.position.x;
    }
    protected override void PlayDeathAnimation()
    {
        //Enemy death animations are played manually in timeline cutscene
    }
}
