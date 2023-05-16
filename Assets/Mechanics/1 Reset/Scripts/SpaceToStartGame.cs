using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceToStartGame : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector3 _originalPosition;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _originalPosition = transform.position;
    }

    private void Update()
    {
        //limits velocity - not sure neccessary
        float velocity = Mathf.Clamp(_rigidbody2D.velocity.y,-5,0);
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, velocity);
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        if (_rigidbody2D.simulated)
        {
            _rigidbody2D.simulated = false;
            _rigidbody2D.transform.position = _originalPosition;
            _rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            _rigidbody2D.simulated = true;
        }
    }
    
    
}
