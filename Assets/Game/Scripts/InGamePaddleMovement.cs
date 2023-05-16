using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePaddleMovement : MonoBehaviour
{
    public InGameManager gameManager;
    public float paddleBasicSpeed;
    public float sizeMultiplierScaler;
    public bool isExtra;
    private const int STANDARD_DIRECTION = 1;
    private const int REVERSED_DIRECTION = -1;
    private Rigidbody2D _rigidbody2D;
    private bool _isRightKeyDown;
    private bool _isLeftKeyDown;
    private float _paddleSizeMultiplier;
    private float _paddleSpeedMultiplier;
    private int _movementDirection = 1;

    private void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _paddleSizeMultiplier = gameManager.GetSettings("Paddle Size");
        _paddleSpeedMultiplier = gameManager.GetSettings("Paddle Speed");

        Vector3 paddleSize = gameObject.transform.localScale;
        paddleSize.x += _paddleSizeMultiplier / sizeMultiplierScaler;
        gameObject.transform.localScale = paddleSize;
    }
    
    private void Update()
    {
        _isLeftKeyDown = isExtra ? Input.GetKey(KeyCode.A) : Input.GetKey(KeyCode.LeftArrow);
        _isRightKeyDown = isExtra ? Input.GetKey(KeyCode.D) : Input.GetKey(KeyCode.RightArrow);
    }

    private void FixedUpdate()
    {
        if (_isLeftKeyDown) _rigidbody2D.AddForce(_movementDirection * Vector2.left * (paddleBasicSpeed +
            _paddleSpeedMultiplier*2));
        if (_isRightKeyDown) _rigidbody2D.AddForce(_movementDirection * Vector2.right * (paddleBasicSpeed +
            _paddleSpeedMultiplier*2));
    }

    #region PotionEffects
    
    public void ChangeBasicSpeed(float amount)
    {
        paddleBasicSpeed += amount;
    }

    public void InverseKeys()
    {
        _movementDirection = (_movementDirection == STANDARD_DIRECTION) ? REVERSED_DIRECTION : STANDARD_DIRECTION;
    }
    
    #endregion
}
