using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthBar;
    private int maxHealth = 100;
    private Movement movement;
    public int currentHealth = 100;
    private bool dead = false;
    [SerializeField] private Material normalMat;
    [SerializeField] private Material hurtMat;
    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private AudioSource PlayerHurt;
    [SerializeField] private Animator deathAnim;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioSource DeathSound;

    private void Start()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        deathAnim = GetComponent<Animator>();
        movement = GetComponent<Movement>();
    }
    //Sets the heath of the player
    public void setHealth(int health)
    {
        healthBar.value = health;
    }
    //Reduces health from player
    public void takeDamage(int damage)
    {
        PlayerHurt.Play();
        currentHealth -= damage;
        StartCoroutine(Flash());
        setHealth(currentHealth);
        //Checks if damage is fatal and plays death animation and displays game over screen
        PlayDeath();
    }
    public void heal(int amount) 
    {
        currentHealth += amount;
        setHealth(currentHealth);
    }
    public void PlayDeath() 
    {
        if (currentHealth <= 0 && !dead) 
        {
            movement.canMove = false;
            deathAnim.Play("Death");
            DeathSound.Play();
            dead = true;
            Invoke("SetGameOverScreen", 1f);
        }
    }
    //Pauses all sound and displays game over screen
    public void SetGameOverScreen() 
    {
        AudioListener.pause = true;
        gameOverScreen.SetActive(true);
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
