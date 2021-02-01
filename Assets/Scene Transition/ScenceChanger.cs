using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceChanger : MonoBehaviour
{
    public bool playerInRange;
    public string SceneToLoad;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private bool immediatelyPlayCutScene;

    // Update is called once per frame
    void Update()
    {
        //Checks if scene change is instant or will wait for player feedback first before changing
        if (playerInRange && immediatelyPlayCutScene)
            SceneManager.LoadScene(SceneToLoad);
        else if (playerInRange && !immediatelyPlayCutScene && Input.GetKeyDown("e"))
            SceneManager.LoadScene(SceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            interactPrompt.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerInRange = false;
        }
    }
}
