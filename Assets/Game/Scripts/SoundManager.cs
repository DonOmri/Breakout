using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private const int SOUND_ON = 1;
    private const int SOUND_OFF = 0;
    private GameObject _inGameToggle;
    
    private void Start()
    {
        _inGameToggle = GameObject.Find("SoundToggle");
        _inGameToggle.GetComponent<Toggle>().isOn = AudioListener.volume.Equals(SOUND_ON);
    }

    public void MuteToggle(bool muted)
    {
        AudioListener.volume = muted? SOUND_ON : SOUND_OFF;
    }
}
