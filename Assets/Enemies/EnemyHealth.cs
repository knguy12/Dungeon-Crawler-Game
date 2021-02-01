using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider sliderHealthBar;
    public Text nameText;
    public GameObject enemyHealthbar;
    public Enemy enemy;
    public Material normalMat;
    public Material hurtMat;
    private SpriteRenderer spriteRend;
    
    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        enemyHealthbar.SetActive(false);
    }
    public void setHealthInfo() 
    {
        nameText.text = enemy.enemyName;
        sliderHealthBar.maxValue = maxHealth;
        sliderHealthBar.value = currentHealth;
    }
    public void setHealth(int health)
    {
        sliderHealthBar.value = health;
    }
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(Flash());
        setHealth(currentHealth);
    }
    //Makes enemy flash white whenever they take damage
    IEnumerator Flash()
    {
        spriteRend.material = hurtMat;
        yield return new WaitForSeconds(0.3f);
        spriteRend.material = normalMat;
        yield return new WaitForSeconds(0.3f);
    }
    public int GetCurrentHealth() 
    {
        return currentHealth;
    }
}
