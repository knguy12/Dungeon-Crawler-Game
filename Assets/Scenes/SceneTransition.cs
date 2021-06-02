using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string SceneToLoad;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private bool PlaySceneOnTrigger; //Loads scene immediately without waiting for user input
    private bool playerInRange;

    // Update is called once per frame
    void Update()
    {
        loadScene();
    }
    private void loadScene() 
    {
        if(PlaySceneOnTrigger)
            SceneManager.LoadScene(SceneToLoad);
        else if (playerInRange && Input.GetKeyDown("e")) 
            SceneManager.LoadScene(SceneToLoad);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            playerInRange = true;
            interactPrompt.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            playerInRange = false;
            interactPrompt.SetActive(false);
        }
    }
}
