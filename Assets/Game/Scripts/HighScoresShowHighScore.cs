using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoresShowHighScore : MonoBehaviour
{
    public TextMeshProUGUI[] texts = new TextMeshProUGUI[3];
    public CrossSceneData highScoresData;
    
    void Start()
    {
        int[] highScores = highScoresData.GetHighScores();
        for (int i = 0; i < highScores.Length; ++i)
        {
            texts[i].text = highScores[i].ToString();
        }
    }
}
