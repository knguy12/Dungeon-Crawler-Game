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
    public bool dead = false;

    [SerializeField] private AudioSource EnemyHurt;
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        enemyHealthbar.SetActive(false);
    }
    public void SetHealthInfo() 
    {
        nameText.text = enemy.enemyName;
        sliderHealthBar.maxValue = maxHealth;
        sliderHealthBar.value = currentHealth;
    }
    public void SetHealth(int health)
    {
        sliderHealthBar.value = health;
    }
    public void TakeDamage(int damage)
    {
        EnemyHurt.Play();
        currentHealth -= damage;
        StartCoroutine(Flash());
        SetHealth(currentHealth);
        if (currentHealth <= 0) 
        {
            dead = true;
        }
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
