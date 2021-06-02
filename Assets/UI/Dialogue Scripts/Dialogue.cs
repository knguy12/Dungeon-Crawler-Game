using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogbox;
    public GameObject interactButton;
    public Text dialogText;
    public Text nameText;
    public string[] characterName;
    public string[] dialog;
    public bool playerInRange;
    public bool dialogIsMandatory;
    public bool dialogIsOptional;
    public bool isFinished;
    [SerializeField] private bool playDialogOnlyOnce;
    public int amountOfDialogMandatory = 0;
    public int amountOfDialogOptional = 0;
    public Movement halt;
    public AudioSource typeWriterSound;

    // Start is called before the first frame update
    void Start()
    {
        typeWriterSound = GetComponent<AudioSource>();
        dialogbox.SetActive(false);
        interactButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        displayOptionalDialog();
        displayMandatoryDialog();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if(dialogIsOptional)
              interactButton.SetActive(true);
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            interactButton.SetActive(false);
            playerInRange = false;
            dialogbox.SetActive(false);
        }
    }

    //Cycles through every letter of dialog and creates a type writer effect
    IEnumerator typeWriter(string sentence)
    {
        foreach(char letter in sentence.ToCharArray()) 
        {
            typeWriterSound.Play();
            dialogText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        StopAllCoroutines();
    }

    //Displays optional dialog
    public void displayOptionalDialog() 
    {
        if (Input.GetKeyDown("e") && playerInRange && dialogIsOptional == true && amountOfDialogOptional < dialog.Length )
        {
            interactButton.SetActive(false);
            dialogText.text = "";
            dialogbox.SetActive(true);
           
            //Stops player movement until dialog is completed
            halt.canMove = false;
            StopAllCoroutines();
            StartCoroutine(typeWriter(dialog[amountOfDialogOptional]));
            nameText.text = characterName[amountOfDialogOptional];
            //Checks to make sure amountOfDialogOptional does not exceed the array size and turns off dialog if so
            amountOfDialogOptional++;
        }
        else if (amountOfDialogOptional >= dialog.Length && Input.GetKeyDown("e"))
        {
            if (playDialogOnlyOnce)
                gameObject.SetActive(false);
            dialogbox.SetActive(false);
            interactButton.SetActive(false);
            amountOfDialogMandatory = 0;
            amountOfDialogOptional = 0;
            halt.canMove = true;    
            isFinished = true;
        }
    }
    //Displays mandatory dialog
    public void displayMandatoryDialog()
    {
        if (playerInRange && dialogIsMandatory == true)
        {
            dialogText.text = "";
            dialogbox.SetActive(true);
            halt.canMove = false;
            StartCoroutine(typeWriter(dialog[amountOfDialogMandatory]));
            nameText.text = characterName[amountOfDialogMandatory];
            //Turns off displayMandatoryDialog after first dialog and pushes rest of dialog onto displayOptionalDialog
            dialogIsMandatory = false;
            dialogIsOptional = true;
            amountOfDialogOptional++;
        }
    }

}
