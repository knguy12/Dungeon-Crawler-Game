using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private AudioSource buttonClick;
    public void Respawn() 
    {
        AudioListener.pause = false;
        buttonClick.Play();
        Invoke("loadSceneDelay", 1f);
    }
    private void loadSceneDelay() 
    {
        SceneManager.LoadScene(sceneToLoad);

    }
    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
