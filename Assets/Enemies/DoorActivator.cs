using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivator : MonoBehaviour
{
    //Checks if player has killed all enemies before opening door to allow them 
    //into next section
    private PlayerAttack killCounter;

    [SerializeField] private int amountOfEnemiesNeededToBeKilled;
    [SerializeField] private GameObject dialogToDisable;
    [SerializeField] private GameObject sceneChangerToActivate;
    // Update is called once per frame
    private void Start()
    {
        killCounter = GetComponent<PlayerAttack>();
    }
    void Update()
    {
        if (killCounter.enemyKilledCounter >= amountOfEnemiesNeededToBeKilled) 
        {
            dialogToDisable.SetActive(false);
            sceneChangerToActivate.SetActive(true);
        }
    }
}
