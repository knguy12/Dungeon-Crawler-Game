using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orb : MonoBehaviour
{
    [SerializeField] private GameObject interactButton;
    [SerializeField] protected Animator playerAnimator;
    [SerializeField] protected Animator orbAnimator;
    [SerializeField] protected GameObject player;
    [SerializeField] protected PlayerInventory playerInv;
    [Header("UI Settings")]
    [SerializeField] protected Text orbDescription;
    [SerializeField] protected Image orbProfile;
    [SerializeField] protected Sprite NewImage;
    [SerializeField] protected string orbAbility;
    [SerializeField] AudioSource pickUp;
    [SerializeField] AudioSource powerUp;
    [SerializeField] protected int orbID;

    protected bool playerInRange;
    protected bool pickedUp;
    protected virtual void Start()
    {
        orbAnimator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        checkPlayerPickUp();
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            playerInRange = true;
            interactButton.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            interactButton.SetActive(false);
        }
    }
    private void checkPlayerPickUp()
    {
        if (playerInv.GetInventoryCount() >= 1) 
        {
            return;
        }
        //Checks to make sure that player is in range and player dosent already have an orb in their inventory
        if (playerInRange && Input.GetKeyDown("e"))
        {
            //Sets the UI elements
            orbDescription.text = orbAbility;
            orbProfile.color = Color.white;
            orbProfile.sprite = NewImage;

            //Plays orb pick up animation and adds the orb into the inventory hashset
            orbAnimator.SetTrigger("itemPickedUp");
            pickUp.Play();
            playerInv.AddItem(this.GetType().ToString() + orbID);
        }
       
    }
    public void PlayPowerUpSound() 
    {
        powerUp.Play();
    }
}
