using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGameFromX : MonoBehaviour
{
    public GameObject pauseWindow;
    public new AudioSource audio;
    private const float DELAY_TO_PLAY_SOUND = 0.1f;
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        audio.Play();
        StartCoroutine(WaitToLoad());
    }

    private IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(DELAY_TO_PLAY_SOUND);
        pauseWindow.SetActive(false);
    }
}
