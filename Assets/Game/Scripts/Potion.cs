using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public float slowPaddlePotionEffect;
    public float slowPaddlePotionTime;
    public float inverseKeysPotionTime;
    public int ballsToCreate;
    public AudioSource soundWhenCollected;
    public Sprite emptyBall;
    public Sprite[] auraSprites = new Sprite[2];
    private static bool _effectIsActive;
    private readonly Vector3 _scaleForOriginalSprite = new (0.6f, 0.6f, 0.6f);
    private const int BOTTOM_OF_GAME = -8;
    private const int TOP_OF_GAME = 8;
    private const float DELAY_TO_PLAY_SOUND = 0.3f;
    private const int INITIAL_LIVES = 3;
    private const float TIME_TO_CHANGE_AURA = 0.1f;
    private SpriteRenderer _potionSpriteRenderer;
    private Rigidbody2D _potionRigidbody2D;
    private InGamePaddleMovement _paddleMovement;
    private InGamePaddleMovement _extraPaddleMovement;
    private InGameLivesManager _inGameLivesManager;
    private GameObject _ball;
    private InGameManager _inGameManager;
    private float _timer;

    private void Start()
    {
        _potionSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _potionRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _paddleMovement = GameObject.Find("Paddle").GetComponent<InGamePaddleMovement>();
        if (GameObject.Find("ExtraPaddle") != null)
        {
            _extraPaddleMovement = GameObject.Find("ExtraPaddle").GetComponent<InGamePaddleMovement>();   
        }
        _inGameLivesManager = GameObject.Find("LivesManager").GetComponent<InGameLivesManager>();
        _ball = GameObject.Find("Ball");
        _inGameManager = GameObject.Find("GameManager").GetComponent<InGameManager>();
    }

    private void Update()
    {
        if(transform.position.y < BOTTOM_OF_GAME || transform.position.y > TOP_OF_GAME) Destroy(gameObject);
        if (_effectIsActive && Time.time - _timer > TIME_TO_CHANGE_AURA && !auraSprites.Equals(null))
        {
            _timer = Time.time;
            _inGameManager.ChangeAuraSprite(auraSprites);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        soundWhenCollected.Play();
        
        StartCoroutine(Effect());
    }
    
    private IEnumerator Effect()
    {
        _potionSpriteRenderer.sprite = null;
        _potionRigidbody2D.simulated = false;
        yield return new WaitForSeconds(DELAY_TO_PLAY_SOUND);
        
        switch (gameObject.name)
        {
            case "PotionIce(Clone)": //Extra balls
                ExtraBall();
                break;
            case "PotionIron(Clone)": //Slow paddle
                if (!_effectIsActive)
                {
                    StartCoroutine(PaddleEffect("SlowPaddle",_paddleMovement));
                    if (_extraPaddleMovement != null) 
                    {
                        StartCoroutine(PaddleEffect("SlowPaddle", _extraPaddleMovement));
                    }   
                }
                break;
            case "PotionPink(Clone)": //Extra life
                ExtraLife();
                break;
            case "PotionPoison(Clone)": //Inverse keys
                if (!_effectIsActive)
                {
                    StartCoroutine(PaddleEffect("InverseKeys",_paddleMovement));
                    if (_extraPaddleMovement != null) 
                    {
                        StartCoroutine(PaddleEffect("InverseKeys", _extraPaddleMovement));
                    }   
                }
                break;
            default:
                throw new ArgumentException("Wrong potion name inserted");
        }
    }
    
    private void ExtraBall() 
    {
        for (int i = 0; i < ballsToCreate; ++i)
        {
            GameObject newBall = Instantiate(_ball);
            newBall.GetComponent<SpriteRenderer>().sprite = emptyBall;
            newBall.transform.localScale = _scaleForOriginalSprite;
        }
    }

    private void ExtraLife()
    {
        if (_inGameLivesManager.GetLives() < INITIAL_LIVES)
        {
            _inGameLivesManager.GetLifeBack();
        }
    }

    private IEnumerator PaddleEffect(string effect, InGamePaddleMovement paddleMovement)
    {
        _effectIsActive = true;
        for (int i = 0; i < _inGameManager.auras.Length; ++i) _inGameManager.auras[i].gameObject.SetActive(true);
        if (effect.Equals("InverseKeys")) paddleMovement.InverseKeys();
        else paddleMovement.ChangeBasicSpeed(-slowPaddlePotionEffect);
        
        if (effect.Equals("InverseKeys")) yield return new WaitForSeconds(inverseKeysPotionTime);
        else yield return new WaitForSeconds(slowPaddlePotionTime);
        
        for (int i = 0; i < _inGameManager.auras.Length; ++i) _inGameManager.auras[i].gameObject.SetActive(false);
        if (effect.Equals("InverseKeys")) paddleMovement.InverseKeys();
        else paddleMovement.ChangeBasicSpeed(slowPaddlePotionEffect);
        _effectIsActive = false;
        
        Destroy(gameObject);
    }
}
