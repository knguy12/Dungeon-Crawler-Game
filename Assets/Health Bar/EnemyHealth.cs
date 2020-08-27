using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider healthBar;
    public Text nameText;
    public GameObject enemyHealthbar;
    public EnemyAi enemy;
    public string enemyName;
    public bool enemyIsDead = false;
    public int previousHealth;
    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
        enemyHealthbar.SetActive(false);
        nameText.text = enemyName;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }
    private void Update()
    {
       
    }
    public void setHealth(int health)
    {
        healthBar.value = health;
    }
    public void takeDamage(int damage)
    {
        previousHealth = currentHealth;
        currentHealth -= damage;
        setHealth(currentHealth);
    }
    public void enemyDie()
    {
        if (currentHealth <= 0)
        {
            enemyIsDead = true;
            enemy.enemyController.SetTrigger("IsDead");
            enemy.enemyController.SetBool("isAttacking", false);
            enemy.attackPower = 0;
            enemy.moveSpeed = 0.0f;
        }
    }
}
