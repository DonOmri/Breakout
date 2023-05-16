using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu]
public class CrossSceneData : ScriptableObject
{
    [SerializeField] private float paddleSizeValue;
    [SerializeField] private float paddleSpeedValue;
    [SerializeField] private float ballSizeValue;
    [SerializeField] private float ballSpeedValue;
    [SerializeField] private int players;
    private readonly int[] _highScores = new int[3];
    private const int DIFFICULTY_SCALER = 4;
    private const int DIFFICULTY_TO_DECREASE_FROM = 11;

    public float PaddleSizeValue
    {
        get {return paddleSizeValue;}
        set {paddleSizeValue = value;}
    }
    
    public float PaddleSpeedValue
    {
        get {return paddleSpeedValue;}
        set {paddleSpeedValue = value;}
    }
    
    public float BallSizeValue
    {
        get {return ballSizeValue;}
        set {ballSizeValue = value;}
    }
    
    public float BallSpeedValue
    {
        get {return ballSpeedValue;}
        set {ballSpeedValue = value;}
    }
    
    public int Players
    {
        get {return players;}
        set {players = value;}
    }

    public int[] GetHighScores()
    {
        return _highScores;
    }

    public float GetDifficulty()
    {
        return (DIFFICULTY_TO_DECREASE_FROM - paddleSizeValue + DIFFICULTY_TO_DECREASE_FROM - paddleSpeedValue +
            DIFFICULTY_TO_DECREASE_FROM - ballSizeValue + ballSpeedValue) / DIFFICULTY_SCALER;
    }

    public void CheckForNewHighScore(int score)
    {
        for (int i = 0; i < _highScores.Length; ++i)
        {
            int tempHelper;
            if (score > _highScores[i])
            {
                tempHelper = _highScores[i];
                _highScores[i] = score;
                score = tempHelper;
            }
        }
    }
}
