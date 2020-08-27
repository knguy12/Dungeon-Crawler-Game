using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceChanger : MonoBehaviour
{
    public bool playerInRange;
    public string SceneToLoad;

    // Update is called once per frame
    void Update()
    {
        if (playerInRange == true && Input.GetKeyDown("e"))
        {
            SceneManager.LoadScene(SceneToLoad);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
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
