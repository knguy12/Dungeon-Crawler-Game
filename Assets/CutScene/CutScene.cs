using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    private PlayableDirector director;
    [Header("Automatic Cutscene after Battle Settings")]
    [SerializeField] private bool cutSceneAfterBattle;
    [SerializeField] private int enemiesNeededForPlay; //enemies needed to be killed before cut scene plays
    [SerializeField] private PlayerAttack playerKillCount; //current player kills

    [SerializeField] Camera cutSceneZoomIn; 
    public bool cutSceneHasFinished;
    private void Start()
    {
        director = GetComponent<PlayableDirector>();
        cutSceneZoomIn.enabled = true;
        cutSceneHasFinished = false;
    }
    private void Update()
    {
        director.stopped += Director_stopped;
        //Checks if their is an immediate cut scene after player kills certain amount of enemies
        //and constantly checks if player has killed enough before playing cutscene
        if (cutSceneAfterBattle)
            StartCoroutine(checkIfAllEnemiesAreDead());
    }
    //Turns off cutscene camera and gameobject once scene is finished
    private void Director_stopped(PlayableDirector obj)
    {
        cutSceneHasFinished = true;
        cutSceneZoomIn.enabled = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            director.Play();
    }
    IEnumerator checkIfAllEnemiesAreDead() 
    {
        if (playerKillCount.enemyKilledCounter >= enemiesNeededForPlay) 
        {
            yield return new WaitForSeconds(1f);
            director.Play();
        }
            
    }
}