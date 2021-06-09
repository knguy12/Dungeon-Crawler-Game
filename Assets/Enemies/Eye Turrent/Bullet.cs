using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private int damage;

    private void Start()
    {
        StartCoroutine(SelfDestruct());
    }
    //Self Destructs the bullet in 5 seconds just in case if it were to get stuck in a loop
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        playBulletImpact();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ensures the bullet does not get destroyed from colliding with triggers or the enemy firing it
        if (!collision.CompareTag("Shooting Enemies") && !collision.CompareTag("Enemy Detector"))
        {
            //Checks if player collides with bullet and reduce health accordingly
            if (collision.CompareTag("Player"))
            {
                Health playerHealth = collision.GetComponent<Health>();
                playerHealth.takeDamage(damage);
            }
            playBulletImpact();
        }
    }
    //Instantiates impact animation and destroy both bullet animation and bullet afterwards
    private void playBulletImpact() 
    {
        GameObject Effect = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(Effect, 0.5f);
        Destroy(gameObject);
    }
}
