using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaddle : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;
    private bool _isRightKeyDown;
    private bool _isLeftKeyDown;
    
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        _isLeftKeyDown = Input.GetKey(KeyCode.LeftArrow);
        _isRightKeyDown = Input.GetKey(KeyCode.RightArrow);
    }

    private void FixedUpdate()
    {
        if (_isLeftKeyDown) _rigidbody2D.AddForce(Vector2.left*10);
        if (_isRightKeyDown) _rigidbody2D.AddForce(Vector2.right*10);
    }
}
