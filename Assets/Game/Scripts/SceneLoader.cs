using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public new AudioSource audio;
    private const float DELAY_TO_PLAY_SOUND = 0.1f;

    public void LoadScene(string sceneName)
    {
        audio.Play();
        Time.timeScale = 1;
        StartCoroutine(WaitToLoad(sceneName));
    }

    private IEnumerator WaitToLoad(string sceneName)
    {
        yield return new WaitForSeconds(DELAY_TO_PLAY_SOUND);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
