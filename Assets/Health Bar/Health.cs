using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthBar;
    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
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
        currentHealth -= damage;
        setHealth(currentHealth);
    }

   
}
