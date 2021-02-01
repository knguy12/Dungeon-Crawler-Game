using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthBar;
    private int maxHealth = 100;
    private int currentHealth = 100;
    [SerializeField] private Material normalMat;
    [SerializeField] private Material hurtMat;
    [SerializeField] private SpriteRenderer spriteRend;

    private void Start()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }
    //Sets the heath of the player
    public void setHealth(int health)
    {
        healthBar.value = health;
    }
    //Reduces health from player
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(Flash());
        setHealth(currentHealth);
    }
    public void heal(int amount) 
    {
        currentHealth += amount;
        setHealth(currentHealth);
    }
    //Flashes white if the player is hurt
    IEnumerator Flash()
    {
        spriteRend.material = hurtMat;
        yield return new WaitForSeconds(0.3f);
        spriteRend.material = normalMat;
        yield return new WaitForSeconds(0.3f);
    }
}
