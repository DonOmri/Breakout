#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    [SerializeField] private CrossSceneData settingsData;
    private const int DEFAULT_VALUE = 1;

    public void Quit()
    {
        ResetSettings();
        
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ResetSettings()
    {
        settingsData.PaddleSizeValue = DEFAULT_VALUE;
        settingsData.PaddleSpeedValue = DEFAULT_VALUE;
        settingsData.BallSizeValue = DEFAULT_VALUE;
        settingsData.BallSpeedValue = DEFAULT_VALUE;
        settingsData.Players = DEFAULT_VALUE;
    }
}
