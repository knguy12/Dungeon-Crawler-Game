using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    private Rigidbody2D rb;
    private UnityEngine.Vector2 movement;
    public Animator enemyController;
    public Transform target;
    public EnemyHealth enemyHealth;
    public Health player;
    public Dialogue dialogBeforeFight;
    public GameObject enemy;
    public int attackPower;
    
    public float moveSpeed = 3f;
    public float lastAttacked = -9999;
    public float attackSpeed = 100;
    public float oldPosition;
    public float newPosition;
    
    public bool canAttack;
    public bool characterHasDialog;
    public bool playerInRange;
    public bool currentlyAttacking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyAttacking)
            enemyController.SetBool("isAttacking", true);
        else
            enemyController.SetBool("isAttacking", false);


        oldPosition = enemy.transform.position.x;
        if (oldPosition > newPosition)
        {
            enemyController.SetFloat("Magnitude", 1);
            enemyController.SetFloat("Horizontal", 1);
        }
        if (oldPosition < newPosition)
        {
            enemyController.SetFloat("Magnitude", 1);
            enemyController.SetFloat("Horizontal", 0);
        }

        CanIAttack();
    }

    private void LateUpdate()
    {
        newPosition = enemy.transform.position.x;
    }
    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }
    void MoveCharacter(UnityEngine.Vector2 direction)
    {
        rb.MovePosition((UnityEngine.Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    public void CanIAttack() 
    {
        if (characterHasDialog == false)
            canAttack = true;
        if (characterHasDialog == true && dialogBeforeFight.isFinished == true)
        {
            canAttack = true;
            characterHasDialog = false;
        }
        //Checks to see if dialog box is active before allowing enemies to attack
        if (dialogBeforeFight.isFinished == false)
            enemyController.SetFloat("Magnitude", 0);
        else {
            UnityEngine.Vector3 Direction = target.position - transform.position;
            Direction.Normalize();
            movement = Direction;
        }
    }
    public void Attack()
    {
        if (Time.time > lastAttacked + attackSpeed)
        {
            //if (player.currentHealth > 0)
            //{
            //    currentlyAttacking = true;
            //    player.takeDamage(attackPower);
            //}
            //lastAttacked = Time.time;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && canAttack)
        {
            Attack();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentlyAttacking = false;
    }
}
