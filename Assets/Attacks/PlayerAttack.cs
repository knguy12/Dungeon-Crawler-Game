using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Build;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public GameObject enemyHealthBar;
    public Rigidbody2D playerBody;
    public Transform attackPoint;
    public Movement direction;
    public Animator attackAnimation;
    public EnemyAi enemy;


    public UnityEngine.Vector3 LeftAttackPoint;
    public UnityEngine.Vector3 TopAttackPoint;
    public UnityEngine.Vector3 BottomAttackPoint;
    public UnityEngine.Vector3 RightAttackPoint;
   
    public LayerMask enemyLayers;
    
    public float attackRange = 0.78f;
    public float attackSpeed = 2f;
    public float nextAttackTime;
   
    public int  playerAttackDamage = 5;
    
    public bool inRangeToAttack;
    public bool enemyHit;
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        enemyHealthBar.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Changes the attack point depending on the orientation of player
        LeftAttackPoint = new UnityEngine.Vector3(playerBody.position.x - 1f, playerBody.position.y, 0);
        TopAttackPoint = new UnityEngine.Vector3(playerBody.position.x , playerBody.position.y + 1f, 0);
        BottomAttackPoint = new UnityEngine.Vector3(playerBody.position.x, playerBody.position.y - 1f, 0);
        RightAttackPoint = new UnityEngine.Vector3(playerBody.position.x + 1f, playerBody.position.y, 0);

        if (direction.LastMoveX == -1)
            attackPoint.position = LeftAttackPoint;
        if (direction.LastMoveY == 1)
            attackPoint.position = TopAttackPoint;
        if (direction.LastMoveY == -1)
            attackPoint.position = BottomAttackPoint;
        if (direction.LastMoveX == 1)
            attackPoint.position = RightAttackPoint;
        //Delays the attack of player depnding on their attackspeed
        if (Time.time > nextAttackTime)
        {
            if (Input.GetKeyDown("space"))
            {
                UserAttack();
                playerBody.velocity = UnityEngine.Vector3.zero;
                nextAttackTime = Time.time + 1 / attackSpeed;
                //Turns on healthbar if enemy is hit and turns it off if they die
                if (enemyHit)
                {
                    enemyHealthBar.SetActive(true);
                    if (enemyHealth.healthBar.value <= 0)
                    {
                        enemyHealthBar.SetActive(false);
                    }
                }
            }
        }
    }
    private void UserAttack()
    {
        attackAnimation.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemies in hitEnemies)
        {
            Debug.Log("Hit Enemy:" + enemies.name);
            enemyHit = true;
            enemyHealth.takeDamage(playerAttackDamage);
            enemyHealth.enemyDie();
            enemy.enemyController.SetTrigger("IsAttacked");
        }
    }
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
