using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Build;
using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject enemyHealthBar;
    private Rigidbody2D playerBody;
    public Transform attackPoint;
    public Movement direction;
    public Animator attackAnimation;
    public AudioManager aud;

    public LayerMask enemyLayers;
    
    public float attackRange = 0.78f;
    public float attackSpeed = 2f;
    public float nextAttackTime;
   
    public int playerAttackDamage = 5;
    
    public bool inRangeToAttack;
    public bool enemyHit;
    public int enemyKilledCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioManager>();
        playerBody = GetComponent<Rigidbody2D>();
        enemyHealthBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        AttackPoint();
        AttackSpeed();
    }
    //Changes the attack point depending on the orientation of player
    private void AttackPoint() 
    {
        UnityEngine.Vector3 LeftAttackPoint = new UnityEngine.Vector3(playerBody.position.x - 1f, playerBody.position.y, 0);
        UnityEngine.Vector3 TopAttackPoint = new UnityEngine.Vector3(playerBody.position.x, playerBody.position.y + 1f, 0);
        UnityEngine.Vector3 BottomAttackPoint = new UnityEngine.Vector3(playerBody.position.x, playerBody.position.y - 1f, 0);
        UnityEngine.Vector3 RightAttackPoint = new UnityEngine.Vector3(playerBody.position.x + 1f, playerBody.position.y, 0);

        if (direction.LastMoveX == -1)
            attackPoint.position = LeftAttackPoint;
        if (direction.LastMoveY == 1)
            attackPoint.position = TopAttackPoint;
        if (direction.LastMoveY == -1)
            attackPoint.position = BottomAttackPoint;
        if (direction.LastMoveX == 1)
            attackPoint.position = RightAttackPoint;
    }
    //Delays the attack of player depnding on their attackspeed
    private void AttackSpeed() 
    {
        if (Time.time > nextAttackTime)
        {
            if (Input.GetKeyDown("space"))
            {
                UserAttack();
                playerBody.velocity = UnityEngine.Vector3.zero;
                nextAttackTime = Time.time + 1 / attackSpeed;
            }
        }
    }
    private IEnumerator AttackPause()
    {
        GetComponent<Movement>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Movement>().enabled = true;
    }
    private void UserAttack()
    {
        aud.Play("Player Attack");
        attackAnimation.SetTrigger("Attack");
        StartCoroutine(AttackPause());
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemies in hitEnemies)
        {
            Enemy enemy = enemies.GetComponent<Enemy>();
            EnemyHealth enemyHealth = enemies.GetComponent<EnemyHealth>();
            if (enemy != null && enemyHealth != null && !enemy.Dead())
            {
                //Turns on healthbar if enemy is hit and turns it off if they die
                enemyHealth.SetHealthInfo();
                enemyHealthBar.SetActive(true);

                //Shows damge in health bar and plays hurt animation
                enemyHealth.TakeDamage(playerAttackDamage);

                if (enemy.Dead())
                {
                    enemyHealthBar.SetActive(false);
                    enemyKilledCounter++;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
