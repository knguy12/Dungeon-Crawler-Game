using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudio : MonoBehaviour
{
    [SerializeField] private AudioSource sourceToMute;
    [SerializeField] private GameObject sourceToActivate;
    [SerializeField] private CutScene scene;

    private void Update()
    {
        if (scene.cutSceneHasStarted) 
        {
            sourceToMute.volume = 0;
            if(sourceToActivate != null)
                sourceToActivate.SetActive(true);
        }
    }
}
