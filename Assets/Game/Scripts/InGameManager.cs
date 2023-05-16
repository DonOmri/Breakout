using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public int[] extraSpeedBreakpoints;
    public float extraSpeed;
    public GameObject ball;
    public GameObject livesManager;
    public GameObject gameOverWindow;
    public GameObject pauseWindow;
    public GameObject[] paddles;
    public SpriteRenderer[] auras;
    public Sprite[] auraSprites;
    public TextMeshProUGUI gameOverScore;
    public TextMeshProUGUI gameOverMultiplier;
    public InGameScoreMechanism score;
    [SerializeField] private CrossSceneData settingsData;
    private const int BOTTOM_OF_GAME = -8;
    private const int TOP_OF_GAME = 8;
    private const int SINGLE_PLAYER = 1;
    private const float TIME_TO_CHANGE_AURA = 0.1f;
    private InGameLivesManager _lifeTracker;
    private InGameBallMovement _inGameBallMovement;
    private CreateRandomLevel _createRandomLevel;
    private int _bricksCounter;
    private float _timer;
    private bool _removeAura;

    void Start()
    {
        _lifeTracker = livesManager.GetComponent<InGameLivesManager>();
        _inGameBallMovement = ball.GetComponent<InGameBallMovement>();
        _createRandomLevel = GetComponent<CreateRandomLevel>();

        ActivateGame();
        
        Time.timeScale = 1;
    }
    
    private void Update()
    {
        int currentLives = _lifeTracker.GetLives();
        if (currentLives != 0 && (ball.transform.position.y < BOTTOM_OF_GAME || ball.transform.position.y > 
                TOP_OF_GAME))
        {
            _lifeTracker.LifeTracker();
            
            if (currentLives - 1 == 0) GameOver();
            
            else _inGameBallMovement.RestartBall();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) PauseAndPlayGame();

        if (_bricksCounter == 0 && _inGameBallMovement.bounceBack)
        {
            StartCoroutine(ResetGame());
            _timer = Time.time;
        }

        if (!_inGameBallMovement.bounceBack && Time.time - _timer > TIME_TO_CHANGE_AURA)
        {
            _timer = Time.time;
            _removeAura = true;
            for (int i = 0; i < paddles.Length; ++i)
            {
                auras[i].gameObject.SetActive(true);
            }
            
            ChangeAuraSprite(auraSprites);
        }
        
        if (_inGameBallMovement.bounceBack && auras[0].gameObject.activeSelf && _removeAura)
        {
            _removeAura = false;
            for (int i = 0; i < paddles.Length; ++i) auras[i].gameObject.SetActive(false);
        }
    }

    private void ActivateGame() // regarding number of players
    {
        if (settingsData.Players == SINGLE_PLAYER)
        {
            GameObject.Find("ExtraPaddle").SetActive(false);
            GameObject.Find("UpperBorder").SetActive(true);
        }
        else
        {
            GameObject.Find("ExtraPaddle").SetActive(true);
            GameObject.Find("UpperBorder").SetActive(false);
        }
    }
    private void GameOver()
    {
        gameOverWindow.SetActive(true);
        ball.SetActive(false);
        foreach (GameObject paddle in paddles)
        {
            paddle.SetActive(false);   
        }

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject singleBall in balls)
        {
            Destroy(singleBall);
        }
        UpdateAndShowFinalScore();
    }
    
    private void PauseAndPlayGame()
    {
        if (pauseWindow.activeSelf)
        {
            pauseWindow.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pauseWindow.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public float GetSettings(string type)
    {
        switch (type)
        {
            case "Paddle Size": return settingsData.PaddleSizeValue;
            case "Paddle Speed": return settingsData.PaddleSpeedValue;
            case "Ball Size": return settingsData.BallSizeValue;
            case "Ball Speed": return settingsData.BallSpeedValue;
            default: throw new ArgumentException("Invalid setting");
        }
    }

    private void UpdateAndShowFinalScore()
    {
        float finalScore = score.GetScore()*settingsData.GetDifficulty();
        gameOverMultiplier.text = settingsData.GetDifficulty().ToString();
        gameOverScore.text = ((int) finalScore).ToString();
        settingsData.CheckForNewHighScore((int) finalScore);
    }

    public void ChangeBricksCount(bool increase)
    {
        if (increase) ++_bricksCounter;
        else
        {
            --_bricksCounter;
            foreach (int speedBreakpoint in extraSpeedBreakpoints)
            {
                if (_bricksCounter == speedBreakpoint) _inGameBallMovement.IncreaseBallSpeedMultiplier(extraSpeed);
            }
        }
    }

    private IEnumerator ResetGame()
    {
        _inGameBallMovement.bounceBack = false;
        
        yield return new WaitUntil(() => _inGameBallMovement.rigidbody2D.simulated == false);
        
        _createRandomLevel.CreateLevel();
        
        _inGameBallMovement.bounceBack = true;
        _timer = 0;
    }

    public void ChangeAuraSprite(Sprite[] effectAuras)
    {
        for (int i=0; i<auras.Length; ++i) auras[i].sprite = auras[i].sprite.Equals(effectAuras[0]) ?
            effectAuras[1] : effectAuras[0];
    }
}