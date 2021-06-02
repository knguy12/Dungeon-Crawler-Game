using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicFadeOut : MonoBehaviour
{
    public int secondsToFadeOut = 5;
    public AudioSource audioMusic;

    public void findAudio()
    {
        // Call findAudioAndFadeOut Coroutine
        StartCoroutine(findAudioAndFadeOut());
    }

    IEnumerator findAudioAndFadeOut()
    {

        // Check Music Volume and Fade Out
        while (audioMusic.volume > 0.01f)
        {
            audioMusic.volume -= Time.deltaTime / secondsToFadeOut;
            yield return null;
        }

        // Make sure volume is set to 0
        audioMusic.volume = 0;

        // Stop Music
        audioMusic.Stop();

        // Destroy
        Destroy(this);
    }
}
