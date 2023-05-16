using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameScoreMechanism : MonoBehaviour
{
    public TextMeshProUGUI gameScoreText;
    private int _score;

    public int GetScore()
    {
        return _score;
    }

    public void IncreaseScore(int amount)
    {
        _score += amount;
        gameScoreText.text = _score.ToString();
    }
}
