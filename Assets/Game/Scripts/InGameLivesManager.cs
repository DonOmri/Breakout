using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class InGameLivesManager : MonoBehaviour
{
    public GameObject[] hearts = new GameObject[3];
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public AudioSource lostLifeAudio;
    public AudioSource gameOverAudio;
    private const int FULL_LIVES = 3;
    private int _lives = 3;

    public void LifeTracker()
    {
        hearts[FULL_LIVES - _lives--].GetComponent<Image>().sprite = emptyHeart;
        if (_lives > 0) lostLifeAudio.Play();
        else gameOverAudio.Play();
    }

    public int GetLives()
    {
        return _lives;   
    }

    public void GetLifeBack()
    {
        hearts[FULL_LIVES-++_lives].GetComponent<Image>().sprite = fullHeart;
    }
}
