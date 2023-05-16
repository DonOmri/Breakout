using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCorrectMultiplayer : MonoBehaviour
{
    [SerializeField] private CrossSceneData settingsData;
    private const int SINGLE_PLAYER = 1;
    private const int MULTI_PLAYER = 2;
    private Toggle _inGameToggle;
    
    void Start()
    {
        _inGameToggle = GameObject.Find("PlayersToggle").GetComponent<Toggle>();
        if (settingsData.Players == SINGLE_PLAYER) return;
        _inGameToggle.isOn = false;
        settingsData.Players = MULTI_PLAYER;
    }

    public void MultiplayerToggle()
    {
        settingsData.Players = settingsData.Players == SINGLE_PLAYER ? MULTI_PLAYER : SINGLE_PLAYER;
    }
}
